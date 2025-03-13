using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public class FormularioVentasModel
    {
        public string Cliente { get; set; }
        public string Categoria { get; set; }
        public string Agente { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Articulo { get; set; }
        public string Cantidad { get; set; }
        public string ObservacionesGenerales { get; set; }
    }
}
