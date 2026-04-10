namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class PresentacionMenuDTO
    {
        public long IdPresentacionProducto { get; set; }
        public long IdProducto { get; set; }
        public string? DetallePresentacion { get; set; }
        public double Precio { get; set; }
        
    }
}
