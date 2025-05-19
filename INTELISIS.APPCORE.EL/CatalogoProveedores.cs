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
        public int proveedorID { get; set; }
        [Required(ErrorMessage = "Favor de llenar todos los datos requeridos")]
        public string NombreProveedor { get; set; }
        public string TipoProveedor { get; set; }
        public string RFC { get; set; }
        public int Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Estatus { get; set; }


    }
}
