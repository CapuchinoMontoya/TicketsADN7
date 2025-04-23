using INTELISIS.APPCORE.EL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("EstadosTicket")]
    public class EstadoTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoID { get; set; }

        [Required(ErrorMessage = "El nombre del estado es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre del estado no puede exceder 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion del estado es obligatorio")]
        [StringLength(50, ErrorMessage = "La descripcion del estado no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }
        public bool EsCerrado { get; set; } = false;
    }
}