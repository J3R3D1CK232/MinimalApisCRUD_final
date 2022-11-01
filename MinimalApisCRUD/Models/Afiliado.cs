using System;
using System.Collections.Generic;

namespace MinimalApisCRUD.Models
{
    public partial class Afiliado
    {
        public Afiliado()
        {
            Transaccions = new HashSet<Transaccion>();
        }

        public int IdAfiliado { get; set; }
        public string? PNombre { get; set; }
        public string? SNombre { get; set; }
        public string? PApellido { get; set; }
        public string? SApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? NoTelefono { get; set; }
        public DateTime? FechaIniciocobertura { get; set; }
        public decimal? MontoCobertura { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
