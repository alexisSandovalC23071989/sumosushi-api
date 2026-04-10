namespace SumoSushiApp_BackEnd.DTOs.Promocion
{
    public class PromocionDTO
    {
        public long IdPromocion { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double Precio { get; set; }
        public string? ImagenUrl { get; set; }
        public bool? CanjeablePuntos { get; set; }
        public string Estado { get; set; } = default!;
    }
}