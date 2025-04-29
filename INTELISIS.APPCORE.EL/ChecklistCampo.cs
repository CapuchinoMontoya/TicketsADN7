using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    public class ChecklistCampo
    {
        public int ChecklistCampoID { get; set; }

        [ForeignKey("Checklist")]
        public int ChecklistID { get; set; } 

        [Required]
        [StringLength(100)]
        public string NombreCampo { get; set; }

        [Required]
        public TipoCampo Tipo { get; set; } // Enum abajo

        public bool RequiereEvidencia { get; set; } = false;

        public virtual Checklist Checklist { get; set; }
    }

    public enum TipoCampo
    {
        Checkbox = 1,
        Texto = 2,
        Cantidad = 3,
        Fecha = 4,
        Archivo = 5
    }
}
