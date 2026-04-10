namespace SumoSushiApp_BackEnd.DTOs.Puntos
{
    public class PuntosClienteDTO
    {
        public ulong PuntosAcumulados { get; set; }
        public int PuntosMinimosCanje { get; set; }
        public int PuntosMaximosCanje { get; set; }
    }
}