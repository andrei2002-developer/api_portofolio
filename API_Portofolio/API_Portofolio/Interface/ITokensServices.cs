using API_Portofolio.Database.Entities;
using ErrorOr;

namespace API_Portofolio.Interface
{
    public interface ITokensServices
    {
        ErrorOr<string> GenerateJwtToken(string username);
        Task<ErrorOr<string>> GenerateRefreshToken(Account user);
        Task<ErrorOr<bool>> ValidateRefreshTokenAsync(string refreshToken);
    }
}
