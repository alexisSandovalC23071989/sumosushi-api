namespace SumoSushiApp_BackEnd.DTOs.Reporte
{
    public class MetodoPagoUsoDto
    {
        public int IdMetodoPago { get; set; }
        public string MetodoPago { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PorcUso { get; set; }
    }
}