using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Rol;
using SumoSushiApp_BackEnd.Repositories.Interfaces;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly IDBContext _dbContext;
        public RolRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RolDTO>> ObtenerRoles()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<RolDTO>("ObtenerRoles", commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
