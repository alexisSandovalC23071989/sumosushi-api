namespace SumoSushiApp_BackEnd.DTOs.Reporte
{
    public class ProductPerformanceDto
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; } = null!;
        public int UnidadesVendidas { get; set; }
        public decimal Ingresos { get; set; }
        public decimal PorcContribucion { get; set; }
    }
}