using API_Portofolio.Interface;
using ErrorOr;

namespace API_Portofolio.Services
{
    public class MailServices : IMailServices
    {
        public Task<ErrorOr<bool>> SendContactAsync(string email, string message)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<bool>> SendNotificationAsync(string email, string body)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<bool>> SendResetMailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
