using System;
using System.Collections.Generic;

namespace MinimalApisCRUD.Models
{
    public partial class Transaccion
    {
        public int IdTransaccion { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdAfiliado { get; set; }
        public DateTime? FechaCobertura { get; set; }
        public string? Respuesta { get; set; }
        public DateTime? FechaConsulta { get; set; }

        public virtual Afiliado? IdAfiliadoNavigation { get; set; }
        public virtual Proveedor? IdProveedorNavigation { get; set; }
    }

    public record TransaccionRequest( int? IdProveedor, int? IdAfiliado, DateTime? FechaCobertura, String? Respuesta, DateTime? FechaConsulta);
}
