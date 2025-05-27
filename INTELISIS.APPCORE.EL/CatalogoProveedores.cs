using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    [Table("CatalogoProveedores")]
    public class CatalogoProveedores
    {
        [Key]
        public int ProveedorID { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string NombreProveedor { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string TipoProveedor { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string RFC { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string CodigoPostal { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Estatus { get; set; } = "Activo";


    }
}
