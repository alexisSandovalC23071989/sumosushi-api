namespace SumoSushiApp_BackEnd.DTOs.Carrito
{
    public class CarritoAgregarDTO
    {
        public long IdUsuario { get; set; }
        public int? IdPresentacionProducto { get; set; }
        public long? IdPromocion { get; set; }
        public int Cantidad { get; set; }

    }
}
