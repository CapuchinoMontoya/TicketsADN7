using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    [Table("Equipos")]
    public class Equipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipoID { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre del activo no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [Column(TypeName = "Text")]
        public string Descripcion { get; set; }

        [StringLength(50, ErrorMessage = "La marca no puede exceder 50 caracteres")]
        public string Marca { get; set; }

        [StringLength(50, ErrorMessage = "El modelo no puede exceder 50 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El numero de serie del equipo es obligatorio")]
        [StringLength(50, ErrorMessage = "El número de serie no puede exceder 50 caracteres")]
        public string NumeroSerie { get; set; }

        public int? DepartamentoID { get; set; }

        public int? UsuarioID { get; set; }

        public string? CodigoQr { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("DepartamentoID")]
        public virtual Departamento Departamento { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}