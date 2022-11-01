using System;
using System.Collections.Generic;

namespace MinimalApisCRUD.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Transaccions = new HashSet<Transaccion>();
        }

        public int IdProveedor { get; set; }
        public int? Nit { get; set; }
        public string? RazonSocial { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
