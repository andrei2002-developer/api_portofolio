using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;

using API_Portofolio.Models.Chat.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Threading.Tasks;

[Authorize]
public class ChatHub : Hub
{
    private readonly DatabaseContext _dbContext;
    private readonly IChatServices _chatServices;

    public ChatHub(DatabaseContext dbContext, IChatServices chatServices)
    {
        _dbContext = dbContext;
        _chatServices = chatServices;
    }

    private static readonly Dictionary<string, List<string>> ChatHistory = new();

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Identity?.Name ?? "Anonim";
        await Clients.Caller.SendAsync("ReceiveMessageConnect", "Server", $"Te-ai conectat ca {userName}");
    }

    public async Task LoadChatHistory(Guid chatId)
    {
        var chat = _dbContext.Chats.FirstOrDefault(u => u.Chat_ReferenceCode == chatId);

        var messages = await _dbContext.Messages
             .Where(m => m.Chat_Id == chat.Chat_Id)
             .OrderBy(m => m.Message_SendTime)
             .Select(m => new MessageListResponse { 
                MessageText = m.Message_Text,
                IsAuthor = true,
                DateTimeSend = m.Message_SendTime.ToShortTimeString()
             })
             .ToListAsync();

        await Clients.Caller.SendAsync("ChatHistory", messages);
    }

    public async Task SendMessage(Guid chatId, string message)
    {
        try
        {
            var userName = Context.User?.Identity?.Name ?? "Anonim";

            var chat = _dbContext.Chats.FirstOrDefault(u => u.Chat_ReferenceCode == chatId);

            var chatMessage = new Message
            {
                Message_Id = Guid.NewGuid(),
                Message_ReferenceCode = Guid.NewGuid(),
                UserID = userName, // De Modificat
                Message_SendTime = DateTime.UtcNow,
                Chat_Id = chat.Chat_Id,
                Message_Text = message
            };

            _dbContext.Messages.Add(chatMessage);
            await _dbContext.SaveChangesAsync();

            var response = new MessageListResponse()
            {
                MessageText = chatMessage.Message_Text,
                DateTimeSend = chatMessage.Message_SendTime.ToShortTimeString(),
                IsAuthor = true
            };

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userName, response);
        }
        catch(Exception ex)
        {

        }
    }
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        await Clients.Caller.SendAsync("ReceiveMessageConnect", "Server", $"Ai intrat în chat-ul {chatId}");
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
}
