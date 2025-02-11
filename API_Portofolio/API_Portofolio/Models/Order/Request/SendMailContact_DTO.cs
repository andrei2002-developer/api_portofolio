namespace API_Portofolio.Models.Order.Request
{
    public class SendMailContact_DTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Message { get; set; } = null!;    
    }
}
