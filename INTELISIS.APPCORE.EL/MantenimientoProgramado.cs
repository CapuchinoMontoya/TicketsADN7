using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTELISIS.APPCORE.EL
{
    public class MantenimientoProgramado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }

        [Required]
        public int FrecuenciaDias { get; set; }

        public DateTime? FechaUltimaRevision { get; set; }

        [Required]
        public DateTime FechaProximaRevision { get; set; }

        public bool Activo { get; set; } = true;
    }
}
