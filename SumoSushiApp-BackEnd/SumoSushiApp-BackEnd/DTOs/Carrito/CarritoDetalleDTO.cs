namespace SumoSushiApp_BackEnd.DTOs.Carrito
{
    public class CarritoItemDTO
    {
        public long IdProductoCarritoUsuario { get; set; }
        public int? IdPresentacionProducto { get; set; }
        public long? IdPromocion { get; set; }    
        public int Cantidad { get; set; }
    }
}
