using Microsoft.AspNetCore.Identity;

namespace API_Portofolio.Database.Entities
{
    public class Account : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool AcceptedTerms { get; set; } = false;
        public bool Active { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Chat> ChatsAsUser1 { get; set; } = new List<Chat>();
        public ICollection<Chat> ChatsAsUser2 { get; set; } = new List<Chat>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
