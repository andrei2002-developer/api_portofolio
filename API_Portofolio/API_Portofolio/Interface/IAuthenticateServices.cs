using API_Portofolio.Database.Entities;
using API_Portofolio.Models.Authenticate.Internal;
using API_Portofolio.Models.Authenticate.Request;
using API_Portofolio.Models.Authenticate.Response;
using ErrorOr;

namespace API_Portofolio.Interface
{
    public interface IAuthenticateServices
    {
        Task<ErrorOr<bool>> CreateAccountAsync(CreateAccountRequest request);
        Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ErrorOr<RefreshResponse>> RefreshTokenAsync(string refreshToken);
        Task<ErrorOr<bool>> ChangePasswordAsync(Account user, ChangePasswordRequest request);
        Task<ErrorOr<bool>> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
