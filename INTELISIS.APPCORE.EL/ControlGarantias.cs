using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    [Table("ControlGarantias")]
    public class ControlGarantias
    {
        [Key]
        public int GarantiaID { get; set; }
        [ForeignKey("Equipos")]
        public int EquipoID { get; set; }
        [ForeignKey("CatalogoProveedores")]
        public int ProveedorID { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaInicioGarantia { get; set; }
        public DateTime FechaFinGarantia { get; set; }
        public string TipoGarantia { get; set; }
        public string Detalles { get; set; }
        public string EstadoGarantia { get; set; }

        [NotMapped]
        public CatalogoProveedores Proveedor { get; set; }
    }
}
