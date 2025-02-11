namespace API_Portofolio.Models.Chat.Response
{
    public class MessageListResponse
    {
        public string MessageText { get; set; } = null!;
        public bool IsAuthor { get; set; }
        public string DateTimeSend { get; set; } = null!;

    }
}
