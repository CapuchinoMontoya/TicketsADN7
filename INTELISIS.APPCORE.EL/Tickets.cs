using INTELISIS.APPCORE.EL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("Tickets")]
    public class Ticket
    {
        public int TicketID { get; set; }
        [Required(ErrorMessage = "El titulo del ticket es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de la prioridad no puede exceder 50 caracteres")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaCierre { get; set; } = DateTime.Now;
        [ForeignKey("CategoriaTicket")]
        public int CategoriaID { get; set; }
        [ForeignKey("Prioridad")]
        public int PrioridadID { get; set; }
        [ForeignKey("EstadoTicket")]
        public int EstadoID { get; set; } = 1; // Por defecto Abierto
        [ForeignKey("Usuario")]
        public int UsuarioReporteID { get; set; }
        [ForeignKey("Usuario")]
        public int? UsuarioAsignadoID { get; set; }
        [ForeignKey("Departamento")]
        public int? DepartamentoID { get; set; }
        

        // Propiedades de navegación
        public virtual CategoriaTicket Categoria { get; set; }
        public virtual Prioridad Prioridad { get; set; }
        public virtual EstadoTicket Estado { get; set; }
        public virtual Usuario UsuarioReporte { get; set; }
        public virtual Usuario UsuarioAsignado { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual TicketChecklist TicketChecklist { get; set; }

    }
}