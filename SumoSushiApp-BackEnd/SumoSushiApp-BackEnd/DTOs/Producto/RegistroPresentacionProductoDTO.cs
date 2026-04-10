using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class RegistroPresentacionProductoDTO
    {
        public long IdProducto { get; set; }
        [Required(ErrorMessage = "El IdPresentacion es obligatorio.")]
        public int IdPresentacion { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public double Precio { get; set; }

    }
}
