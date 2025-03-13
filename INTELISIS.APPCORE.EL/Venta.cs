using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Venta
    {
        public int ID { get; set; }

        public string? Agente { get; set; }

        public string? Estatus { get; set; }

        public string? Mov { get; set; }

        public string? MovID { get; set; }

        public DateTime? FechaEmision { get; set; }

        public string? Cliente { get; set; }

        public decimal? Importe { get; set; }

        public int? Ejercicio { get; set; }

        public int? Periodo { get; set; }
        public int? Sucursal { get; set; }

        public string FechaEmisionFormateada => FechaEmision?.ToString("dd-MM-yyyy") ?? string.Empty;

    }
}