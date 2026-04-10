namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IHashService
    {
        public string HashContrasenna(string password);
        public bool VerificarContrasenna(string password, string hashedPassword);
    }
}
