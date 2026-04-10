namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class ActualizarDatosProductoDTO
    {
        public long IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public bool CanjeablePuntos { get; set; }
        public int IdCategoria { get; set; }
        public string? Estado { get; set; }

        public ActualizarDatosProductoDTO(ActualizarProductoDTO model)
        {
            IdProducto = model.IdProducto;
            Nombre = model.Nombre;
            Descripcion = model.Descripcion;
            ImagenUrl = model.ImagenUrl;
            CanjeablePuntos = model.CanjeablePuntos;
            IdCategoria = model.IdCategoria;
            Estado = model.Estado;
        }

    }
}
