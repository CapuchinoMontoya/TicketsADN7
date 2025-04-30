using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("CategoriasTickets")]
    public class CategoriaTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre de la categoría no puede exceder 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción de la categoría es obligatorio")]
        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}