using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;

using API_Portofolio.Models.Chat.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Threading.Tasks;

[Authorize]
public class ChatHub : Hub
{
    private readonly DatabaseContext _dbContext;
    private readonly IChatServices _chatServices;
    private readonly UserManager<Account> _userManager;

    public ChatHub(DatabaseContext dbContext, IChatServices chatServices, UserManager<Account> userManager)
    {
        _dbContext = dbContext;
        _chatServices = chatServices;
        _userManager = userManager;
    }

    private static readonly Dictionary<Guid, HashSet<string>> ChatConnections = new();
    public async Task LoadChatHistory(Guid chatId)
    {
        var username = Context.User?.Identity?.Name ?? "Anonim";

        var user = await _userManager.FindByNameAsync(username ?? string.Empty);

        if (user == null)
        {
            return;
        }

        var chat = await _dbContext.Chats.FirstOrDefaultAsync(u => u.Chat_ReferenceCode == chatId);

        var messages = await _dbContext.Messages
             .Where(m => m.Chat_Id == chat.Chat_Id)
             .OrderBy(m => m.Message_SendTime)
             .Select(m => new MessageListResponse
             {
                 MessageText = m.Message_Text,
                 IsAuthor = user.Id == m.UserID ? true : false,
                 DateTimeSend = m.Message_SendTime.ToShortTimeString()
             }).ToListAsync();

        await Clients.Caller.SendAsync("ChatHistory", messages);
    }
    public async Task SendMessage(Guid chatId, string message)
    {
        var username = Context.User?.Identity?.Name ?? "Anonim";

        var user = await _userManager.FindByNameAsync(username ?? string.Empty);

        if (user == null)
        {
            return;
        }

        var chat = _dbContext.Chats.FirstOrDefault(u => u.Chat_ReferenceCode == chatId);

        var chatMessage = new Message
        {
            Message_Id = Guid.NewGuid(),
            Message_ReferenceCode = Guid.NewGuid(),
            UserID = user.Id, // De Modificat
            Message_SendTime = DateTime.UtcNow,
            Chat_Id = chat.Chat_Id,
            Message_Text = message
        };

        _dbContext.Messages.Add(chatMessage);
        await _dbContext.SaveChangesAsync();

        var responseForSender = new MessageListResponse()
        {
            MessageText = chatMessage.Message_Text,
            DateTimeSend = chatMessage.Message_SendTime.ToShortTimeString(),
            IsAuthor = true
        };

        var responseForReceiver = new MessageListResponse()
        {
            MessageText = chatMessage.Message_Text,
            DateTimeSend = chatMessage.Message_SendTime.ToShortTimeString(),
            IsAuthor = false
        };

        // Obținem toate conexiunile din grup
        if (ChatConnections.TryGetValue(chatId, out var connections))
        {
            foreach (var connectionId in connections)
            {
                var isSender = connectionId == Context.ConnectionId;
                var response = isSender ? responseForSender : responseForReceiver;
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", username, response);
            }
        }
    }
    public async Task JoinChat(Guid chatId)
    {
        lock (ChatConnections)
        {
            if (!ChatConnections.ContainsKey(chatId))
                ChatConnections[chatId] = new HashSet<string>();

            ChatConnections[chatId].Add(Context.ConnectionId);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        await Clients.Caller.SendAsync("ReceiveMessageConnect", "Server", $"Ai intrat în chat-ul {chatId}");
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        lock (ChatConnections)
        {
            foreach (var chat in ChatConnections.Values)
            {
                chat.Remove(Context.ConnectionId);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
}
