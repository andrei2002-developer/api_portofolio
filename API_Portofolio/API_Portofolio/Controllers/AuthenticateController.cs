using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Models.Authenticate.Request;
using API_Portofolio.Services;
using ErrorOr;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace API_Portofolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokensServices _tokensService;
        private readonly IAuthenticateServices _authenticateService;
        private readonly IAntiforgery _antiForgery;
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IChatServices _chatServices;
        private readonly IMailServices _mailServices;

        public AuthenticateController(ITokensServices tokensService, IAuthenticateServices authenticateService, IAntiforgery antiForgery, UserManager<Account> userManager, ILogger<AuthenticateController> logger, IChatServices chatServices, IMailServices mailServices)
        {
            _tokensService = tokensService;
            _authenticateService = authenticateService;
            _antiForgery = antiForgery;
            _userManager = userManager;
            _logger = logger;
            _chatServices = chatServices;
            _mailServices = mailServices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticateService.CreateAccountAsync(request);

            if (result.IsError)
            {
                var firstError = result.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "An unknown error has occurred" })
                };
            }

            //var createChat = await _chatServices.CreatePersonalChatAsync("");

            return Ok("Account created successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                //_logger.LogWarning("Numele de utilizator si parola nu sunt completate");
                return BadRequest(ModelState);
            }

            var response = await _authenticateService.LoginAsync(request);

            if (response.IsError)
            {
                var firstError = response.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    ErrorType.Unauthorized => Unauthorized( new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "A apărut o eroare necunoscută" })
                };
            }

            Response.Cookies.Append("jwtToken",response.Value.JwtToken, new CookieOptions
            {
                HttpOnly = true, // cookie-ul nu va fi accesibil din JavaScript
                Secure = false, // Asigură-te că folosești HTTPS
                SameSite = SameSiteMode.Lax, // Permite trimiterea pe cereri cross-origin
                Expires = DateTime.UtcNow.AddMinutes(30) // Setează expirarea cookie-ului
            });

            Response.Cookies.Append("refreshToken", response.Value.RefreshToken.ToString(), new CookieOptions
            {
                HttpOnly = true, // cookie-ul nu va fi accesibil din JavaScript
                Secure = false, // Asigură-te că folosești HTTPS
                SameSite = SameSiteMode.Lax, // Permite trimiterea pe cereri cross-origin
                Expires = DateTime.UtcNow.AddDays(30) // Setează expirarea cookie-ului
            });

            return Ok(new { name = response.Value.Username, role = response.Value.Rol });
        }

        [HttpGet("is-authenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            // Verifică dacă refresh token-ul este în cookie-uri
            if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                var response = await _authenticateService.RefreshTokenAsync(refreshToken);

                if (response.IsError)
                {
                    var firstError = response.Errors.First();

                    return firstError.Type switch
                    {
                        ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                        ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                        _ => Unauthorized(new { message = "Autentificare nereușită." })
                    };
                }

                Response.Cookies.Append("jwtToken", response.Value.JwtToken, new CookieOptions
                {
                    HttpOnly = true, // cookie-ul nu va fi accesibil din JavaScript
                    Secure = false, // Asigură-te că folosești HTTPS
                    SameSite = SameSiteMode.Lax, // Permite trimiterea pe cereri cross-origin
                    Expires = DateTime.UtcNow.AddMinutes(30) // Setează expirarea cookie-ului
                });

                Response.Cookies.Append("refreshToken", response.Value.RefreshToken.ToString(), new CookieOptions
                {
                    HttpOnly = true, // cookie-ul nu va fi accesibil din JavaScript
                    Secure = false, // Asigură-te că folosești HTTPS
                    SameSite = SameSiteMode.Lax, // Permite trimiterea pe cereri cross-origin
                    Expires = DateTime.UtcNow.AddDays(30) // Setează expirarea cookie-ului
                });

                return Ok(new { name = response.Value.Username, role = response.Value.Rol });
            }

            return Unauthorized(new { message = "Refresh token-ul este lipsă." });
        }

        [Authorize, HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Nu există un utilizator autentificat.");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized("Utilizatorul nu a fost găsit.");
            }

            await _userManager.RemoveAuthenticationTokenAsync(user, "Default", "RefreshToken");

            Response.Cookies.Delete("jwtToken");
            Response.Cookies.Delete("refreshToken");

            _logger.LogInformation($"Utilizatorul cu ID-ul {username} s-a delogat.");

            return Ok(new { message = "Sesiunea a fost încheiată cu succes." });
        }

        [Authorize, HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var user = await _userManager.FindByNameAsync(username ?? string.Empty);

            if (user == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _authenticateService.ChangePasswordAsync(user,request);

            return Ok();
        }

        [HttpPost("send-reset-mail")]
        public async Task<IActionResult> ResetPassword([FromBody] string email)
        {
            if (email != string.Empty && email != null){
                return BadRequest("Email input is required!");
            }

            var result = await _mailServices.SendResetMailAsync(email);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }         

            var result = await _authenticateService.ResetPasswordAsync(request);

            return Ok();
        }
    }
}
