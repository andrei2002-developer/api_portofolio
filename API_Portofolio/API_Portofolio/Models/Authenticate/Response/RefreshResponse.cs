﻿namespace API_Portofolio.Models.Authenticate.Response
{
    public class RefreshResponse
    {
        public string Username { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string JwtToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}

