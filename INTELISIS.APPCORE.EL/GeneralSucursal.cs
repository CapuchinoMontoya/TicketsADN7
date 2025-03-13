using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    public partial class GeneralSucursal
    {
        public int? Periodo { get; set; }
        public int? Trimestre { get; set; }
        [DisplayName("Importe Real")]
        public double? VentaReal { get; set; }
        public double? Presupuesto { get; set; }

    }
}