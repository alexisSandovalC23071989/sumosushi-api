namespace SumoSushiApp_BackEnd.DTOs.Reporte
{
    public class TipoPedidoUsoDto
    {
        public int IdTipoPedido { get; set; }
        public string TipoPedido { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PorcUso { get; set; }
    }
}