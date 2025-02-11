using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configurează Serilog folosind un fișier JSON
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Citire din appsettings.json
    .Enrich.FromLogContext() // Adaugă informații contextuale
    .WriteTo.Console() // Loguri în consolă
    .WriteTo.File("Logs/app-log-.txt", rollingInterval: RollingInterval.Day) // Loguri în fișiere, pe zile
    .CreateLogger();

// Adaugă Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5, // Numărul maxim de cereri permise
                Window = TimeSpan.FromSeconds(10), // Fereastra de timp
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2 // Numărul maxim de cereri în coada de așteptare
            });
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
    };
});


// ====================== Servicii Adăugate ======================

// 1. MVC și Opțiuni JSON
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// 2. Servicii personalizate
builder.Services.AddScoped<ITokensServices, TokensServices>();
builder.Services.AddScoped<IAuthenticateServices, AuthenticateServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IChatServices, ChatServices>();
builder.Services.AddScoped<IMailServices, MailServices>();

// 3. Swagger pentru documentarea API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Configurarea bazei de date
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 5. Configurarea autentificării JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configurarea validării token-ului
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };

    // Extrage token-ul din cookie-uri
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("jwtToken", out var jwtToken))
            {
                context.Token = jwtToken;
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // Evită redirecționarea pentru `Unauthorized`
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "Unauthorized" }));
        },
        OnForbidden = context =>
        {
            // Evită redirecționarea pentru `Forbidden`
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "Forbidden" }));
        }
    };
});

// 6. Configurare CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowCredentials() // Permite transmiterea cookie-urilor
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 7. Identity Configurare
builder.Services.AddIdentityCore<Account>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DatabaseContext>()
.AddDefaultTokenProviders();

// ===================== Configurare și Rulare =====================

builder.Services.AddSignalR();

var app = builder.Build();

// Adaugă middleware-ul Rate Limiter
app.UseRateLimiter();

// Migrare bază de date
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();

    var services = scope.ServiceProvider;
    await InitializeRolesAsync(services);
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseRouting();
app.UseAuthentication(); // Autentificare înainte de autorizare
app.UseAuthorization();  // Autorizare
app.MapControllers();    // Maparea endpoint-urilor

app.MapHub<ChatHub>("/chatHub");

app.Run();

// ===================== Funcție pentru inițializare roluri =====================

static async Task InitializeRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<Account>>();

    string[] roleNames = { "Admin", "Client" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
