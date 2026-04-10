using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class DatosProductoDTO
    {
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public string? ImagenURL { get; set; }
        [Required]
        public bool CanjeablePuntos { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}
