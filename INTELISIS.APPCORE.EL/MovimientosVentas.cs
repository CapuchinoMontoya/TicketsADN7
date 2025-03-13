using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class MovimientosVentas
    {
        [DisplayName("Moviemiento")]
        public string Mov { get; set; } = null!;
        [DisplayName("Fecha de Emision")]
        public DateTime? FechaEmision { get; set; }
        [DisplayName("Cliente")]
        public string? Cliente { get; set; }
        [DisplayName("Importe")]
        public decimal? Importe { get; set; }
        [DisplayName("Estatus")]
        public string? Estatus { get; set; }

    }
}