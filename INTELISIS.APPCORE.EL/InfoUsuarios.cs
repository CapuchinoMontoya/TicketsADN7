using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class InfoUsuarios
    {
        public string Usuario { get; set; } = null!;

        public string? FotoPerfil { get; set; }
        public string? Descripcion { get; set; }
    }
}