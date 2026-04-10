namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class ValidacionCambioContrasenna
    {
        public long IdUsuario { get; set; }
        public string? Contrasenna { get; set; }
        public string? Correo { get; set; }
    }
}
