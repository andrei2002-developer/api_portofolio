using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class Chat
    {
        [Key]
        public Guid Chat_Id { get; set; }
        [Required]
        public Guid Chat_ReferenceCode { get; set; }
        [Required]
        public string Chat_Name { get; set; } = null!;
       
        [Required]
        public DateTime CretedTime { get; set; }
        [Required]
        public DateTime LastAccess { get; set; }

        
        [Required(ErrorMessage = "SuportedPlatformId is required")]
        [ForeignKey("SuportedPlatform")]
        public Guid ChatTypeId { get; set; }
        public ChatType ChatType { get; set; } = null!;


        [Required(ErrorMessage = "Id user is required")]
        [ForeignKey("User1")]
        public string IdUser { get; set; } = null!;

        public Account User1 { get; set; } = null!;

        [Required(ErrorMessage = "Id user is required")]
        [ForeignKey("User2")]
        public string IdUser_Secondary { get; set; } = null!;

        public Account User2 { get; set; } = null!;


        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
