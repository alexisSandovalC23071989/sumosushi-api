using SumoSushiApp_BackEnd.DTOs.Carrito;

public interface ICarritoRepository
{
   public  Task<(int CodigoError, string Mensaje)> AgregarOActualizarPromocion(CarritoAgregarDTO dto);
   public  Task<(int CodigoError, string Mensaje)> AgregarOActualizarPresentacion(CarritoAgregarDTO dto);
   public Task<(int CodigoError, string Mensaje)> EliminarProducto(CarritoEliminarDTO dto);
   public Task<IEnumerable<CarritoItemDTO>> ObtenerCarrito(long idUsuario);

}
