using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Models.Authenticate.Request
{
    public class CreateAccountRequest
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este validă.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Parola este obligatorie.")]
        [MinLength(8, ErrorMessage = "Parola trebuie să aibă minim 8 caractere.")]
        public string Password { get; set; } = null!;

        public string First_Name { get; set; } = null!;
        public string Last_Name { get; set; } = null!;
    }
}
