using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class CteRuta
    {
        public int RutaID { get; set; }
        public string Agente { get; set; }

        [Required(ErrorMessage = "El nombre de la ruta es obligatorio.")]
        public string Nombre { get; set; }

        public string DiaRuta { get; set; }

        public string Frecuencia { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string Estatus { get; set; }
    }
}