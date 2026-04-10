using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Services
{
    public class HashService : IHashService
    {
        // Factor de costo (rounds). 12 es un buen balance entre seguridad y rendimiento
        private int WorkFactor;

        public HashService(IConfiguration configuration)
        {
            WorkFactor = int.Parse(configuration["HashContrasenna:WorkFactor"]!);
        }

        public string HashContrasenna(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));

            // bcrypt genera automáticamente un salt único y lo incluye en el hash
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public bool VerificarContrasenna (string? password, string? hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                return false;
            }
        }
    }
}