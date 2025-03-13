using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace INTELISIS.APPCORE.EL
{
    /// <summary>
    /// Clase para el manejo y uso de procedimientos almacenados
    /// </summary>
    public class StoreProcedureParameters
    {
        public string ParamName { get; set; }
        public dynamic ParamValue { get; set; }
        public SqlDbType SqlDbType { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}