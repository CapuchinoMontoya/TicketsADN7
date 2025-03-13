using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class VentaXAgente
    {
        public int? Ejercicio { get; set; }

        public int? Periodo { get; set; }

        public string? Agente { get; set; }

        public int? Trimestre { get; set; }
        public DateTime? FechaActual { get; set; }
    }
}