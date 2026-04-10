using MySqlConnector;
using System.Data;

namespace SumoSushiApp_BackEnd.Data
{
    public interface IDBContext
    {
        public IDbConnection MySqlConexion();
        MySqlConnection CrearConexionMySql();
    }
}
