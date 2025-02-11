using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portofolio.Database.Entities
{
    public class Message
    {
        [Key]
        public Guid Message_Id { get; set; }
        public DateTime Message_SendTime { get; set; }   
        public string Message_Text { get; set; } = null!;          
        public Guid Message_ReferenceCode { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        [ForeignKey("Account")]
        public string UserID { get; set; } = null!;
        public Account Account { get; set; } = null!;

        [Required(ErrorMessage = "Chat is required")]
        [ForeignKey("Chat")]
        public Guid Chat_Id { get; set; }
        public Chat Chat { get; set; } = null!;
    }
}
