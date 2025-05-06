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
        [Required(ErrorMessage = "Favor de llenar todos los datos requeridos")]
        public int ActivoID { get; set; }
        [ForeignKey("Equipos")]
        public int ProveedorID { get; set; }
        [ForeignKey("CatalogoProveedores")]
        public DateTime FechaInicioGarantia { get; set; }
        public int FechaFinGarantia { get; set; }
        public string TipoGarantia { get; set; }
        public string Detalles { get; set; }
        public string EstadoGarantia { get; set; }
    }
}
