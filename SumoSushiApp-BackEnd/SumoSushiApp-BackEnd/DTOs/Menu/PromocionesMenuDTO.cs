namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class PromocionesMenuDTO
    {
        public long IdPromocion { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double Precio { get; set; }
        public string? ImagenUrl { get; set; }
        public bool CanjeablePuntos { get; set; }
        public string? Estado { get; set; }
    }
}
