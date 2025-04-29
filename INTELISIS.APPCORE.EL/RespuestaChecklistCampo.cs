using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    public class RespuestaChecklistCampo
    {
        public int RespuestaChecklistCampoID { get; set; }

        [ForeignKey("TicketChecklist")]
        public int TicketChecklistID { get; set; }

        [ForeignKey("ChecklistCampo")]
        public int ChecklistCampoID { get; set; }

        public string Valor { get; set; } // Se interpreta según el tipo de campo

        public virtual TicketChecklist TicketChecklist { get; set; }
        public virtual ChecklistCampo ChecklistCampo { get; set; }
    }

}
