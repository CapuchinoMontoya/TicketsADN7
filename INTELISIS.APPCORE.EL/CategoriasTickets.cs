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
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        public string Descripcion { get; set; }
    }
}