namespace SumoSushiApp_BackEnd.DTOs.Direccion
{
    public class DireccionListadoDTO
    {
        public long IdDireccion { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public short IdDistrito { get; set; }
        public string Distrito { get; set; } = string.Empty;
        public short IdEstado { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}