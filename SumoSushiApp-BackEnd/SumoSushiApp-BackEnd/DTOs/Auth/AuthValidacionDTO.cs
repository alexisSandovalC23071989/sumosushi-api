namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthValidacionDTO
    {
        public long IdUsuario { get; set; }
        public string? Contrasenna { get; set; }
        public string? Rol { get; set; }
    }
}
