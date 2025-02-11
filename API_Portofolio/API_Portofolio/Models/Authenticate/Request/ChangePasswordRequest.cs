using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Models.Authenticate.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
