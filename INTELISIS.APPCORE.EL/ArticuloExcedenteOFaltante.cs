using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public class ArticuloExcedenteOFaltante
    {
        public string Grupo { get; set; }
        public string Familia { get; set; }
        public string Categoria { get; set; }
        [Key]
        public string Articulo { get; set; }
        public string Descripcion1 { get; set; }
        public string Almacen { get; set; }
        public double ExcedenteOFaltante { get; set; }
    }
}