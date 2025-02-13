using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Models.Authenticate.Internal;
using API_Portofolio.Models.Authenticate.Request;
using API_Portofolio.Models.Authenticate.Response;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API_Portofolio.Services
{
    public class AuthenticateServices : IAuthenticateServices
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokensServices _tokensService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateServices(UserManager<Account> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, ITokensServices tokensService/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _tokensService = tokensService;
            //_httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetClaimsFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Verifică dacă tokenul este valid
            if (!tokenHandler.CanReadToken(token))
            {
                throw new SecurityTokenException("Invalid token");
            }

            // Decodifică tokenul
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extrage și returnează ClaimsPrincipal
            return new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwt"));
        }

        public async Task<ErrorOr<string>> CreateAccountAsync(CreateAccountRequest request)
        {
            var user = new Account
            {
                FirstName = request.First_Name,
                LastName = request.Last_Name,
                UserName = request.Email,
                Email = request.Email,
                Active = true,
                AcceptedTerms = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                // În caz de eroare neașteptată, returnează o eroare
                return Error.Failure(
                    code: "UnexpectedError",
                    description: $"A apărut o eroare neașteptată: {result.Errors}");
            }

            // Verifică dacă rolul există, altfel îl creează
            const string defaultRole = "Admin"; //"Client"; // Rolul implicit
            var roleExists = await _roleManager.RoleExistsAsync(defaultRole);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(defaultRole));
                if (!roleResult.Succeeded)
                {
                    return Error.Failure(
                        code: "RoleCreationError",
                        description: $"Eroare la crearea rolului {defaultRole}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }

            // Atribuirea rolului utilizatorului
            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, defaultRole);
            if (!roleAssignmentResult.Succeeded)
            {
                return Error.Failure(
                    code: "RoleAssignmentError",
                    description: $"Eroare la atribuirea rolului: {string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description))}");
            }

            return user.Id;
        }

        public async Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest request)
        {
            // Verifică dacă utilizatorul există
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return Error.Unauthorized(description: "Datele de conectare nu sunt corecte.");
            }

            // Verifică parola utilizatorului
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return Error.Unauthorized(description: "Datele de conectare nu sunt corecte.");
            }

            // Generează token-ul JWT
            var tokenResult = _tokensService.GenerateJwtToken(user.UserName);
            if (tokenResult.IsError)
            {
                return Error.Failure(description: "A apărut o problemă la generarea token-ului JWT.");
            }

            // Generează și salvează refresh token-ul
            var refreshTokenResult = await _tokensService.GenerateRefreshToken(user);
            if (refreshTokenResult.IsError)
            {
                return Error.Failure(description: "A apărut o problemă la generarea refresh token-ului.");
            }

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", refreshTokenResult.Value);

            // Actualizează data de expirare pentru refresh token
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7); // Exemplu: valabil 7 zile
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return Error.Failure(description: "A apărut o problemă la actualizarea utilizatorului.");
            }

            // Returnează răspunsul de succes
            return new LoginResponse
            {
                Username = user.UserName,
                Rol = "Client",
                JwtToken = tokenResult.Value,
                RefreshToken = refreshTokenResult.Value,
            };
        }

        public async Task<ErrorOr<RefreshResponse>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Iterează prin utilizatorii din baza de date
                var users = await _userManager.Users.ToListAsync();
                Account? user = null;

                foreach (var u in users)
                {
                    var token = await _userManager.GetAuthenticationTokenAsync(u, "Default", "RefreshToken");
                    if (token == refreshToken)
                    {
                        user = u;
                        break;
                    }
                }

                if (user == null)
                {
                    return Error.Unauthorized(description: "Utilizatorul nu a fost găsit sau refresh token-ul este invalid.");
                }

                // Verifică dacă refresh token-ul a expirat
                if (user.RefreshTokenExpiration < DateTime.UtcNow)
                {
                    return Error.Unauthorized(description: "Refresh token-ul a expirat.");
                }

                // Generează un nou token JWT și un nou refresh token
                var newAccessToken = _tokensService.GenerateJwtToken(user.UserName);
                var newRefreshToken = await _tokensService.GenerateRefreshToken(user);

                // Salvează noul refresh token și actualizează data de expirare
                await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", newRefreshToken.Value);
                user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7); // Actualizează expirarea
                await _userManager.UpdateAsync(user);

                // Obține rolurile utilizatorului
                var roles = await _userManager.GetRolesAsync(user);

                // Creează răspunsul
                var response = new RefreshResponse
                {
                    JwtToken = newAccessToken.Value,
                    RefreshToken = newRefreshToken.Value,
                    Username = user.UserName,
                    Rol = roles.FirstOrDefault() // Returnăm primul rol, dacă există
                };

                return response;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new RefreshResponse();
            }
        }

        public Task<ErrorOr<bool>> ChangePasswordAsync(Account user, ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
