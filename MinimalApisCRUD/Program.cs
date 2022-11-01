using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApisCRUD.Context;
using MinimalApisCRUD.Models;
using MinimalApisCRUD.Services;
using MinimalApisCRUD.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Proyecto_3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Mi Buen Seguro, S.A.", Description = "Api Rest de la aseguradora Mi Buen Seguro, S.A. destinada a proveedores." });
}
);


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Mi Buen Seguro V1");

});


app.MapGet("/api/afiliados",async (Proyecto_3Context context) => Results.Ok(await context.Afiliados.ToListAsync()));
app.MapGet("/api/proveedores", async (Proyecto_3Context context) => Results.Ok(await context.Proveedors.ToListAsync()));
app.MapGet("/api/transacciones", async (Proyecto_3Context context) => Results.Ok(await context.Transaccions.ToListAsync()));
app.MapGet("/api/afiliado/{IdAfiliado}/{fechaNacimiento}", async (int IdAfiliado, DateTime fechaNacimiento, Proyecto_3Context context) =>
{
    var afiliado = await context.Afiliados.Where(a => a.IdAfiliado == IdAfiliado && a.FechaNacimiento == fechaNacimiento).FirstOrDefaultAsync();

    if (afiliado !=null) 
    {

        string respuesta = "";
        if (afiliado.Estado == "Activo")
        {
            respuesta = "Cobertura Vigente";
        }
        else
        {
            respuesta = "Sin Cobertura";
        }
    
        return Results.Ok(respuesta); 
    }

    return Results.NotFound("Afiliado no encontrado");

});

app.MapPost("/api/proveedor/{nit}/{IdAfiliado}/{FechaNacimiento}/{FechaCobertura}", async (int nit, int IdAfiliado, DateTime FechaNacimiento,DateTime FechaCobertura,  ITransaccionService transaccionService, Proyecto_3Context context) =>
{
    var Proveedor = await context.Proveedors.Where(p => p.Nit == nit).FirstOrDefaultAsync();
    if (Proveedor != null) {
        
        var afiliado = await context.Afiliados.Where(a => a.IdAfiliado == IdAfiliado && a.FechaNacimiento == FechaNacimiento && a.FechaIniciocobertura == FechaCobertura).FirstOrDefaultAsync();

        if (afiliado != null)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (afiliado.Estado == "Activo")
            {
                string cadenaAutorizacion = new string(Enumerable.Repeat(chars, 45)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                TransaccionRequest request = new TransaccionRequest(Proveedor.IdProveedor, IdAfiliado, FechaCobertura, cadenaAutorizacion, System.DateTime.Now);

                var createTransaccion = await transaccionService.CrearTransaccion(request);

                return Results.Ok( cadenaAutorizacion);
            }
            else
            {
                return Results.Ok("Afiliado sin Cobertura");
            }
            
        }
        else 
        {
            return Results.NotFound("Afiliado no encontrado");
        }
    }
    else 
    {
        return Results.NotFound("Proveedor no encontrado");
    }

});

app.Run();
