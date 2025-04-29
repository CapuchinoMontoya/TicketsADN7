using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    public class TicketChecklist
    {
        public int TicketChecklistID { get; set; }

        [ForeignKey("Ticket")]
        public int TicketID { get; set; }

        [ForeignKey("Checklist")]
        public int ChecklistID { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual Checklist Checklist { get; set; }

        public virtual ICollection<RespuestaChecklistCampo> Respuestas { get; set; }
    }

}
