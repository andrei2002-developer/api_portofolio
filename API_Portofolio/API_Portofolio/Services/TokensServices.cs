using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Portofolio.Services
{
    public class TokensServices : ITokensServices
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContext _databaseContext;

        public TokensServices(UserManager<Account> userManager , IConfiguration configuration, /*IHttpContextAccessor httpContextAccessor,*/ DatabaseContext databaseContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            //_httpContextAccessor = httpContextAccessor;
            _databaseContext = databaseContext;
        }

        // Generate Tokens

        public ErrorOr<string> GenerateJwtToken(string username)
        {
            try
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User") // Adaugă roluri sau alte revendicări aici
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        public async Task<ErrorOr<string>> GenerateRefreshToken(Account user)
        {
            try
            {
                var randomNumber = new byte[32];
                using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                }

                //var set_refreshTokenSucces = await SetRefreshToken_OnUser(user, Convert.ToBase64String(randomNumber));

                return Convert.ToBase64String(randomNumber);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        // Set on cookie

        //private void SetJwtTokenCookie(string token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = false,
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SameSite = SameSiteMode.None
        //    };

        //    if (_httpContextAccessor.HttpContext != null)
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append("jwtToken", token, cookieOptions);
        //}

        //private void SetRefreshTokenCookie(string token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = false,
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SameSite = SameSiteMode.None
        //    };

        //    if (_httpContextAccessor.HttpContext != null)
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
        //}

        // Validate Tokens 

        public async Task<ErrorOr<bool>> ValidateRefreshTokenAsync(string refreshToken)
        {
            // Căutăm token-ul în tabela de tokens
            var userToken = _databaseContext.UserTokens.FirstOrDefault(u => u.Name == "RefreshToken" && u.Value == refreshToken);

            if (userToken != null)
            {
                // Așteptăm pentru a obține utilizatorul din UserManager
                var user = await _userManager.FindByIdAsync(userToken.UserId);

                if (user != null)
                {
                    // Dacă utilizatorul există, încercăm să-l convertim la tipul Account
                    var userAsAccount = user as Account;

                    if (userAsAccount != null)
                    {
                        // Apelăm metoda de generare a unui nou refresh token, dacă este necesar
                        var generate_Success = await GenerateRefreshToken(userAsAccount);

                        if (generate_Success.IsError)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }     
                    }
                }
            }

            // Dacă nu găsim token-ul sau utilizatorul, returnăm false
            return false;
        }

        // Set Values in Database

        //private async Task<bool> SetRefreshToken_OnUser(Account user, string refreshToken)
        //{
        //    try
        //    {
        //        await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", refreshToken);

        //        return true;
        //    }
        //    catch {
        //        return false;
        //    }
        //}

        // Get Values
    }
}
