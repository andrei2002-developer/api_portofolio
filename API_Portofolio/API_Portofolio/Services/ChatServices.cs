using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Models.Chat.Response;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace API_Portofolio.Services
{
    public class ChatServices : IChatServices
    {
        private readonly DatabaseContext _databaseContext;

        public ChatServices(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ErrorOr<bool>> CreateOrderChatAsync(Account request, int orderNumber)
        {
            try
            {
                await _databaseContext.Chats.AddAsync(new Chat()
                {
                    CretedTime = DateTime.UtcNow,
                    ChatTypeId = new Guid("5bfb2b27-7424-4500-a2cd-d216771307c1"),
                    IdUser = request.Id,
                    IdUser_Secondary = "31b0aec3-2f7f-4504-bfb3-fb2115b04d0d", // id admin
                    Chat_Name = $"Order #{orderNumber}",
                    LastAccess = DateTime.UtcNow
                });

                await _databaseContext.SaveChangesAsync();

                return new ErrorOr<bool>();
            }
            catch(Exception ex)
            {
                return new ErrorOr<bool>();
            }
        }

        public async Task<ErrorOr<bool>> CreatePersonalChatAsync(string idUser)
        {
            await _databaseContext.Chats.AddAsync(new Chat()
            {
                CretedTime = DateTime.UtcNow,
                ChatTypeId = new Guid("60bf9886-9060-4c86-a1c8-85ab67446efa"),
                IdUser = idUser,
                IdUser_Secondary = "31b0aec3-2f7f-4504-bfb3-fb2115b04d0d", // id admin
                Chat_Name = "Personal Conversation",
                LastAccess = DateTime.UtcNow,
            });

            await _databaseContext.SaveChangesAsync();

            return new ErrorOr<bool>();
        }

        public async Task<ErrorOr<List<ChatListResponse>>> GetChatsAsync(Account request)
        {
            var chats = await _databaseContext.Chats
                .Where(u => u.IdUser == request.Id || u.IdUser_Secondary == request.Id)
                .Select(u => new ChatListResponse()
                {
                    Id = u.Chat_ReferenceCode,
                    LastAccess = u.LastAccess.ToShortDateString(),
                    Name = u.Chat_Name,
                    IsOrder = false 
                })
                .ToListAsync();

            if (chats.Any())
            {
                return chats;
            }
            else
            {
                return Error.NotFound(
                   code: "ChatsNotFound",
                   description: "Nu au fost găsite chat-uri");
            }
        }
    }
}
