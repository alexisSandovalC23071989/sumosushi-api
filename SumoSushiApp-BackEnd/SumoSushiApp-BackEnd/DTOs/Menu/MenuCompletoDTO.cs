namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class MenuCompletoDTO
    {
        public long VersionMenu { get; set; }
        public IEnumerable<PromocionesMenuDTO>? Promociones { get; set; }
        public CategoriasMenuDTO[]? Categorias { get; set; }        
    }
}
