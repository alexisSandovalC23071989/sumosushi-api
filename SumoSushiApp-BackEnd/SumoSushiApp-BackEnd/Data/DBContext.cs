using Microsoft.AspNetCore.SignalR;
using MySqlConnector;
using System.Data;

namespace SumoSushiApp_BackEnd.Data
{
    public class DBContext : IDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection MySqlConexion() => new MySqlConnection(_connectionString);

        //CONEXION NECESARIA PARA TRANSACCIONES MULTIPLES ATOMICAS
        public MySqlConnection CrearConexionMySql() => new MySqlConnection(_connectionString);
    }
}