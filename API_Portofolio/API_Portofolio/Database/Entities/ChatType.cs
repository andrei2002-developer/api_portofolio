using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class ChatType
    {
        [Key]
        public Guid ChatTypeId { get; set; }
        public string ChatType_Name { get; set; } = null!;

        public ICollection<Chat> ChatList { get; set; } = new List<Chat>();
    }
}
