namespace SumoSushiApp_BackEnd.Models
{
    public class RespuestaModel
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
        public object? Datos { get; set; }
        public int Alerta { get; set; }
    }
}
