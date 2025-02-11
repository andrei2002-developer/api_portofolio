using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Models.Chat.Response
{
    public class ChatListResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string LastAccess { get; set; }
        [Required]
        public bool IsOrder { get; set; }
    }
}
