using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Models.Authenticate.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public Guid IdResetPassword { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
