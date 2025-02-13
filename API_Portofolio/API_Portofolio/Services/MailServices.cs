using API_Portofolio.Interface;
using API_Portofolio.Models.Order.Request;
using ErrorOr;

namespace API_Portofolio.Services
{
    public class MailServices : IMailServices
    {
        public Task<ErrorOr<bool>> SendContactAsync(SendMailContact_DTO request)
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
