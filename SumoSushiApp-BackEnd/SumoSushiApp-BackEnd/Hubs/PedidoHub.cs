using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SumoSushiApp_BackEnd.Hubs
{
    [Authorize(Roles = "Admin,Colaborador")]
    public class PedidoHub : Hub
    {
        // Opcional: podrías agregar métodos aquí si necesitás que el frontend los llame
    }
}