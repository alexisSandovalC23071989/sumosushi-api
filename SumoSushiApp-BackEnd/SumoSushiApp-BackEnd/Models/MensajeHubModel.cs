namespace SumoSushiApp_BackEnd.Models
{
    public class MensajeHubModel
    {
        public string? Mensaje { get; set; }
        public DateTime Hora { get; set; } = DateTime.Now;

        public MensajeHubModel(string? mensaje)
        {
            Mensaje = mensaje;
        }
    }
}
