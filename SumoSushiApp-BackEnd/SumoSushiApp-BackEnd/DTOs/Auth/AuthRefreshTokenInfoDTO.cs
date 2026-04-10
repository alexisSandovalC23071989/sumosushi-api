namespace SumoSushiApp_BackEnd.DTOs.Token
{
    public class AuthRefreshTokenInfoDTO
    {
        public long IdUsuario { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public int Utilizado { get; set; }
        public bool Revocado { get; set; }
        public string? Rol { get; set; }

    }
}
