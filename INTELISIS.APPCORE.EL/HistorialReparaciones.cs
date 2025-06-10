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
        //[ForeignKey("EquipoID")]
        public int EquipoID { get; set; }
        public string Area { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaReparacion { get; set; }
        public string DescripcionProblema { get; set; }
        public string TrabajoRealizado { get; set; }
        public decimal Costo { get; set; }
        public int ProveedorID { get; set; }
        //[ForeignKey("CatalogoProveedores")]
        public string Responsable { get; set; }
        public virtual CatalogoProveedores Proveedor { get; set; }
        public virtual Equipo Equipo { get; set; }


    }

    public class HistorialReparacionesEditViewModel
    {
        [Key]
        public int ReparacionID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int EquipoID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Area { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaReparacion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string DescripcionProblema { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string TrabajoRealizado { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Valor no válido, debe ser un número decimal válido.")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "El campo Proveedor es obligatorio")]
        public int ProveedorID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Responsable { get; set; }
    }

}
