using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class RegistroProductoDTO
    {
        [Required(ErrorMessage = "El nombre del producto es obigatorio")]
        public string? Nombre { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public IFormFile? ImagenFile { get; set; }
        [Required]
        public bool CanjeablePuntos { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        
        public List<EtiquetaProductoDTO> Etiquetas { get; set; } = new List<EtiquetaProductoDTO>();

        [Required(ErrorMessage ="Las presentaciones son obligatorias")]
        public List<RegistroPresentacionProductoDTO> Presentaciones { get; set; } = new List<RegistroPresentacionProductoDTO>();
        //public List<long> Etiquetas { get; set; } = new List<long>();
        //public List<NuevaPresentacionProductoDTO> Presentaciones { get; set; } = new List<NuevaPresentacionProductoDTO>();
    }
}
