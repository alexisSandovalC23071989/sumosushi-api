using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface ICategoriaService
    {
        public Task<RespuestaModel> ObtenerCategorias();

        public Task<RespuestaModel> ObtenerCategoriasActivas();

        public Task<RespuestaModel> ObtenerCategoriasInactivas();
        public Task<RespuestaModel> DeshabilitarCategoria(int idCategoria);

        public Task<RespuestaModel> CrearCategoria(CategoriaCrearDTO dto);

        public Task<RespuestaModel> ActualizarCategoria(CategoriaActualizarDTO dto);

        public Task<RespuestaModel> HabilitarCategoria(int idCategoria);




    }

}
