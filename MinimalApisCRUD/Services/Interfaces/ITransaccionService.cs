using MinimalApisCRUD.Models;
namespace MinimalApisCRUD.Services.Interfaces
{
    public interface ITransaccionService
    {
        Task<Transaccion> CrearTransaccion(TransaccionRequest  request);
    }
}
