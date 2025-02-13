using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Models.Authenticate.Request
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
