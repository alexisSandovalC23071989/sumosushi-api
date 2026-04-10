namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class ProductosMenuDTO
    {
        public long IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }        
        public bool CanjeablePuntos { get; set; }
        public long IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public string? Estado { get; set; }
        public string? Etiquetas { get; set; }
        //public double Calificacion { get; set; }
        public IEnumerable<PresentacionMenuDTO>? Presentaciones { get; set; }
    }
}
