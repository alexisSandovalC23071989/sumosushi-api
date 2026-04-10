using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class NuevoProductoDTO
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public bool CanjeablePuntos { get; set; }
        public int IdCategoria { get; set; }

        public NuevoProductoDTO(RegistroProductoDTO model, string imagenUrl) 
        {
            Nombre = model.Nombre;
            Descripcion = model.Descripcion;
            ImagenUrl = imagenUrl;
            CanjeablePuntos = model.CanjeablePuntos;
            IdCategoria = model.IdCategoria;
        }
    }
}
