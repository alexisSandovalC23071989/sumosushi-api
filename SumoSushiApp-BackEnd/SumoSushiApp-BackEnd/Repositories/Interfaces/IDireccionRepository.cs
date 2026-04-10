using SumoSushiApp_BackEnd.DTOs.Direccion;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IDireccionRepository
    {
        /// Obtiene todas las direcciones (activas e inactivas) de un usuario,
        /// incluyendo detalle de distrito y descripción de estado.
        Task<IEnumerable<DireccionCompletaDTO>> ObtenerDireccionesUsuario(long idUsuario);

        /// Inserta una nueva dirección para el usuario (Estado = Activo).
        Task<(int CodigoError, string Mensaje)> AgregarDireccionUsuario(long idUsuario, DireccionCrearDTO dto);

        /// Deshabilita la dirección anterior (Estado = Inactivo) y agrega una nueva como Activo.
        Task<(int CodigoError, string Mensaje)> ActualizarDireccionUsuario(long idUsuario, DireccionActualizarDTO dto);

        /// Deshabilita una dirección existente (Estado = Inactivo).
        Task<(int CodigoError, string Mensaje)> DeshabilitarDireccionUsuario(long idUsuario, long idDireccion);
        
        Task<IEnumerable<CantonDTO>> ObtenerCantones();
        
        Task<IEnumerable<DistritoDTO>> ObtenerDistritosPorCanton();
    }
}