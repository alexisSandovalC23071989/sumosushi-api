using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Promocion
{
    public class PromocionCrearDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "Canjeablo por puntos es obligatorio")]
        public bool CanjeablePuntos { get; set; }
        [Required(ErrorMessage = "La imagen es obligatoria")]
        public IFormFile? ImagenFile { get; set; }
        public string? ImagenUrl { get; set; }
          
        
            
    }
}