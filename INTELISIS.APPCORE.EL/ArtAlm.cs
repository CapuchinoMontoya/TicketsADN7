using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{

    public partial class ArtAlm
    {
        public string Empresa { get; set; } = null!;

        public string Articulo { get; set; } = null!;

        public string SubCuenta { get; set; } = null!;

        public string Almacen { get; set; } = null!;

        public string? Localizacion { get; set; }

        public string? Pasillo { get; set; }

        public string? Anaquel { get; set; }

        public string? Estante { get; set; }

        public double? Minimo { get; set; }

        public double? Maximo { get; set; }

        public double? PuntoOrden { get; set; }

        public double? PuntoOrdenOrdenar { get; set; }

        public string? LoteOrdenar { get; set; }

        public double? CantidadOrdenar { get; set; }

        public double? MultiplosOrdenar { get; set; }

        public double? CantidadOrdenarTiempo { get; set; }

        public DateTime? UltimoCambio { get; set; }

        public DateTime? UltimoMovimiento { get; set; }

        public bool? TieneMovimientos { get; set; }

        public bool? AbastecimientoDirecto { get; set; }

        public double? MinimoTarima { get; set; }

        public string? Abc { get; set; }

        public double? VentaPromedio { get; set; }

        public double TiempoEntrega { get; set; }
    }
}