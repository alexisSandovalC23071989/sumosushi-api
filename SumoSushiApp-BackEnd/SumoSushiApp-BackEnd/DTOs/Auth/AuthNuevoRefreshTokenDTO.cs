namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthNuevoRefreshTokenDTO
    {
        public long IdUsuario { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }
}
