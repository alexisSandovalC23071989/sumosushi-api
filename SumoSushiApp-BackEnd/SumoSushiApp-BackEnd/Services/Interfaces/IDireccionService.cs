using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IDireccionService
    {
        /// Retorna todas las direcciones (activas/inactivas) de un usuario.
        Task<RespuestaModel> ObtenerDireccionesUsuario(long idUsuario);

        /// Agrega una nueva dirección para el usuario.
        Task<RespuestaModel> AgregarDireccion(long idUsuario, DireccionCrearDTO dto);

        /// Actualiza (reemplaza) una dirección existente del usuario.
        Task<RespuestaModel> ActualizarDireccion(long idUsuario, DireccionActualizarDTO dto);

        /// Deshabilita (inactiva) una dirección existente del usuario.
        Task<RespuestaModel> DeshabilitarDireccion(long idUsuario, long idDireccion);
        
        Task<RespuestaModel> ObtenerCantones();

        Task<RespuestaModel> ObtenerDistritos();
    }
}