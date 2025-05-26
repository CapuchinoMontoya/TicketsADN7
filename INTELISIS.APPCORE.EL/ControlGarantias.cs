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
        public CatalogoProveedores Proveedor { get; set; }
    }

    public class ControlGarantiasEditViewModel
    {
        [Key]
        public int GarantiaID { get; set; }
        [Required]
        public int EquipoID { get; set; }
        [Required]
        public int ProveedorID { get; set; }
        [Required]
        public DateTime FechaCompra { get; set; }
        [Required]
        public DateTime FechaInicioGarantia { get; set; }
        [Required]
        public DateTime FechaFinGarantia { get; set; }
        [Required]
        public string TipoGarantia { get; set; }
        [Required]
        public string Detalles { get; set; }
        [Required]
        public string EstadoGarantia { get; set; }
    }
}
