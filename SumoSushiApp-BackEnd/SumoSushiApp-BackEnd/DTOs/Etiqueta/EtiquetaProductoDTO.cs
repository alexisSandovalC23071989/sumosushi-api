using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Etiqueta
{
    public class EtiquetaProductoDTO
    {
        [Required(ErrorMessage = "El IdEtiqueta es obligatorio.")]
        public long IdEtiqueta { get; set; }
        public string? Etiqueta { get; set; }
        public long IdProducto { get; set; }
    }
}
