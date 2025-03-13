using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class ClientesPerdidos
    {
        [DisplayName("No.Cliente")]
        public string Cliente { get; set; } = null!;
        [DisplayName("Cliente")]
        public string? Nombre { get; set; }
    }
}