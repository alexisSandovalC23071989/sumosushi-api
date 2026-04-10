namespace SumoSushiApp_BackEnd.DTOs.Direccion
{
    public class DireccionCompletaDTO
    {
        public long IdDireccion { get; set; }
        public string? Detalle { get; set; }
        public int IdCanton { get; set; }
        public string? Canton { get; set; }
        public int IdDistrito { get; set; }
        public string? Distrito { get; set; }        
    }
}