using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class CteVisita
    {
        public int VisitaID { get; set; }
        public string Agente { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaVisita { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string Estatus { get; set; }
        public string FotoEvidencia { get; set; }
    }
}