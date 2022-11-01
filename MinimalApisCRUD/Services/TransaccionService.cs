using MinimalApisCRUD.Context;
using MinimalApisCRUD.Models;
using MinimalApisCRUD.Services.Interfaces;
using System.Data;

namespace MinimalApisCRUD.Services

{
    public class TransaccionService : ITransaccionService
    {
        private readonly Proyecto_3Context _context;

        public TransaccionService(Proyecto_3Context context)
        {
            _context = context;

        }

        public async Task<Transaccion> CrearTransaccion(TransaccionRequest request)
        {
            var transaccion = new Transaccion
            {
                IdProveedor = request.IdProveedor ,
                IdAfiliado = request.IdAfiliado,
                FechaCobertura = request.FechaCobertura,
                Respuesta = request.Respuesta,
                FechaConsulta = request.FechaConsulta,

            };

            var createTransaccion = await _context.Transaccions.AddAsync(transaccion);
                await _context.Transaccions.AddAsync(transaccion);

            await _context.SaveChangesAsync();

            return createTransaccion.Entity;
        }
    }
}
