using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class PrecioArtCodigoBarras
    {
        public decimal PrecioBase { get; set; }
        public decimal TasaIVA { get; set; }
        public decimal TasaIEPS { get; set; }
        public decimal SubtotalConIEPS { get; set; }
        public decimal PrecioConImpuestos { get; set; }
    }
}