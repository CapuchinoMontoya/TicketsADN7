using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class VentaD
    {
        public int VentaId { get; set; }

        public string? Articulo { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }
    }
}