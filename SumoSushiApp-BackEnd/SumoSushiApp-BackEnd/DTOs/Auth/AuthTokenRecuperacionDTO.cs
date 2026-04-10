namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthTokenRecuperacionDTO
    {
        public string? Correo { get; set; }
        public string? Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
