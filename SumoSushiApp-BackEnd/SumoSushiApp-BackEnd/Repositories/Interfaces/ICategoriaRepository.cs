using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
       
        
            public Task<IEnumerable<CategoriaDTO>> ObtenerCategorias();
            public Task<IEnumerable<CategoriaDTO>> ObtenerCategoriasActivas();
            public Task<IEnumerable<CategoriaDTO>> ObtenerCategoriasInactivas();
            public Task<(int CodigoError, string Mensaje)> DeshabilitarCategoria(int idCategoria);
            public Task<(int CodigoError, string Mensaje)> AgregarCategoria(CategoriaCrearDTO dto);
            public Task<(int CodigoError, string Mensaje)> ActualizarCategoria(CategoriaActualizarDTO dto);
            public Task<(int CodigoError, string Mensaje)> HabilitarCategoria(int idCategoria);
          









    }



}

