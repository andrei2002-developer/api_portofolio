using ErrorOr;

namespace API_Portofolio.Interface
{
    public interface IMailServices
    {
        Task<ErrorOr<bool>> SendResetMailAsync(string email);
        Task<ErrorOr<bool>> SendNotificationAsync(string email,string body);
        Task<ErrorOr<bool>> SendContactAsync(string email, string message);
    }
}
