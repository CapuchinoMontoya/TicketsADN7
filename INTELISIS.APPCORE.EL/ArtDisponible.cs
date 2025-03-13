using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class ArtDisponible
    {
        public string Empresa { get; set; } = null!;

        public string Articulo { get; set; } = null!;

        public string Almacen { get; set; } = null!;

        public double? Disponible { get; set; }

        public double? Apartado { get; set; }

        public double? DispMenosApartado { get; set; }
    }
}