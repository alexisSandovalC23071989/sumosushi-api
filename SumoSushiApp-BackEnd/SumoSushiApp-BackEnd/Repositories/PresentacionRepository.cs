using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Presentacion;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using System.Xml.Serialization;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class PresentacionRepository : IPresentacionRepository
    {
        private readonly DBContext _dbContext;

        public PresentacionRepository(DBContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<PresentacionDTO>> ObtenerEtiquetas()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<PresentacionDTO>(
                "ObtenerPresentaciones", 
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
