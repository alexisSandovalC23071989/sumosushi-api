namespace SumoSushiApp_BackEnd.DTOs.Reporte
{
    public class GeneralPerformanceDto
    {
        public int UsuariosActivos { get; set; }
        public decimal PromedioConsumo { get; set; }
        public decimal TotalIngresos { get; set; }
        public long TotalPuntosCanjeados { get; set; }
        public decimal TotalGanancia { get; set; }
    }
}