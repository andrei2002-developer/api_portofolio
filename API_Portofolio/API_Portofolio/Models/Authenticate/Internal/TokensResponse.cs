namespace API_Portofolio.Models.Authenticate.Internal
{
    public class TokensResponse
    {
        public string JwtToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string CsrfToken { get; set; } = null!;  
    }
}
