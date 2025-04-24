using INTELISIS.APPCORE.EL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("Prioridades")]
    public class Prioridad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrioridadID { get; set; }

        [Required(ErrorMessage = "El nombre de la prioridad es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de la prioridad no puede exceder 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nivel de la prioridad es obligatorio")]
        public int Nivel { get; set; }
        [Required(ErrorMessage = "La descripcion de la prioridad es obligatorio")]
        [StringLength(50, ErrorMessage = "La descripcion de la prioridad no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }
    }
}