using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    public class Checklist
    {
        public int ChecklistID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación con los campos del checklist
        public virtual ICollection<ChecklistCampo> Campos { get; set; }
    }
}
