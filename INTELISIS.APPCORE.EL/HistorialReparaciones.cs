using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    [Table("HistorialReparaciones")]
    public class HistorialReparaciones
    {
        [Key]
        public int ReparacionID { get; set; }
        [Required(ErrorMessage = "Favor de llenar todos los datos requeridos")]
        public int EquipoID { get; set; }
        [ForeignKey("Equipos")]
        public string Area { get; set; }
        public DateTime FechaReparacion { get; set; }
        public string DescripcionProblema { get; set; }
        public string TrabajoRealizado { get; set; }
        public decimal Costo { get; set; }
        public int ProveedorID { get; set; }
        [ForeignKey("CatalogoProveedores")]
        public string Responsable { get; set; }

    }
}
