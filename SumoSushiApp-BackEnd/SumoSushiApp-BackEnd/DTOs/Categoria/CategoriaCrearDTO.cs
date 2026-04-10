using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Categoria
{
    public class CategoriaCrearDTO
    {
        [Required]
        public string? Descripcion { get; set; }


    }
}
