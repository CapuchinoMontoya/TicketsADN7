using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    /// <summary>
    /// Clase para el manejo de respuestas 
    /// </summary>
    public class ResponseData
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public dynamic ObjectData { get; set; }


        public ResultadoAfectar ResultadoAfeccion { get; set; }
        public List<ResultadoAfectar> ResultadoAfeccionList { get; set; } = new List<ResultadoAfectar>();
    }
}