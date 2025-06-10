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
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int EquipoID { get; set; }
        [ForeignKey("CatalogoProveedores")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int ProveedorID { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicioGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string TipoGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Detalles { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string EstadoGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public CatalogoProveedores Proveedor { get; set; }
        public Equipo Equipo { get; set; }
    }

    public class ControlGarantiasEditViewModel
    {
        [Key]
        public int GarantiaID { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int EquipoID { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int ProveedorID { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime FechaCompra { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime FechaInicioGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime FechaFinGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string TipoGarantia { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Detalles { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string EstadoGarantia { get; set; }
    }
}
