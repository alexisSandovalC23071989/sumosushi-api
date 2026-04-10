namespace SumoSushiApp_BackEnd.DTOs.Direccion
{
    public class DireccionActualizarDTO
    {
        public long IdDireccionAnterior { get; set; }
        public short IdDistritoNuevo { get; set; }
        public string DetalleNuevo { get; set; } = string.Empty;
    }
}