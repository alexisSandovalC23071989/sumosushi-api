namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class ProductoFormDTO
    {
        public string modelJson { get; set; } = string.Empty;
        public IFormFile? ImagenFile { get; set; } = null;
    }
}
