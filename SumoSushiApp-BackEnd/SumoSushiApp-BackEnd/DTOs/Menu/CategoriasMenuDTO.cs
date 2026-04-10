namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class CategoriasMenuDTO
    {
        public long IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public IEnumerable<ProductosMenuDTO>? Productos { get; set; }
    }
}
