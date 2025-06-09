using INTELISIS.APPCORE.EL;

namespace TicketsADN7.Models
{
    public class ChecklistContestadoViewModel
    {
        public int TicketId { get; set; }
        public string NombreChecklist { get; set; }
        public string DescripcionChecklist { get; set; }
        public List<CampoChecklistContestado> Campos { get; set; }
    }

    public class CampoChecklistContestado
    {
        public string Nombre { get; set; }
        public TipoCampo Tipo { get; set; }
        public bool RequiereEvidencia { get; set; }
        public string Valor { get; set; }
    }
}
