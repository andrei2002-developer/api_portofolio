using API_Portofolio.Database.Entities;
using API_Portofolio.Models.Chat.Response;
using ErrorOr;

namespace API_Portofolio.Interface
{
    public interface IChatServices
    {
       Task<ErrorOr<bool>> CreateOrderChatAsync(Account request,int orderNumber);
       Task<ErrorOr<bool>> CreatePersonalChatAsync(string idUser);
       Task<ErrorOr<List<ChatListResponse>>> GetChatsAsync(Account request);
    }
}
