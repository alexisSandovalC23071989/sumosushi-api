namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class CierreSesionDTO
    {
        public string? RefreshToken { get; set; }    
        
        public CierreSesionDTO(string refreshToken = "") 
        {
            RefreshToken = refreshToken;
        }
    }

}
