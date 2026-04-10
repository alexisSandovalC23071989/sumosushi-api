﻿using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Promocion
{
    public class PromocionActualizarDTO
    {
        [Required]
        public long IdPromocion { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "ImagenUrl es obligatoria.")]
        public string? ImagenUrl { get; set; }

        public IFormFile? ImagenFile { get; set; }

        [Required(ErrorMessage = "Canejable por puntos es obligatorio")]
        public bool? CanjeablePuntos { get; set; }
        
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string? Estado { get; set; }

    }
}