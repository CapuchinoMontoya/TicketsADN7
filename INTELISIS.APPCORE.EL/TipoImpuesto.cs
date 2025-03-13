using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class TipoImpuesto
    {
        public string TipoImpuesto1 { get; set; } = null!;

        public double? Tasa { get; set; }

        public string? Concepto { get; set; }

        public string? Referencia { get; set; }

        public string? CodigoFiscal { get; set; }

        public string? Tipo { get; set; }

        public bool? TieneMovimientos { get; set; }

        public string? CuentaDeudora { get; set; }

        public string? CuentaAcreedora { get; set; }

        public string? Clave { get; set; }
    }
}