using Microsoft.AspNetCore.Http;

namespace INTELISIS.APPCORE.EL.Common
{
    public class PosicionesAlmacenLayoutParams
    {
        public IFormFile File { get; set; }
        public string Almacen { get; set; }
        public string NombreAlmacen { get; set; }
        public string TipoLayout { get; set; }



        public class MantenimientosInfraestructuraParams
        {
            public int? Id { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Color { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaHasta { get; set; }
            public DateTime? TargetDate { get; set; }
            public string Usuario { get; set; }
            public string Action { get; set; }
            public List<int> Activos { get; set; } = [];
            public string Periodo { get; set; }
            public string Estatus { get; set; }
        }


        public class RequestTokenParams
        {
            public string Token { get; set; }
            public string Firma { get; set; } = null;
            public string ClaimKey { get; set; } = null;
            public string ClaimValue { get; set; } = null;
        }

    }
}
