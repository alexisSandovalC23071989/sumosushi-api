namespace SumoSushiApp_BackEnd.DTOs.Pedido
{
    public class PromocionModel
    {
        public long IdPedido { get; set; }
        public string? NombrePromocion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
