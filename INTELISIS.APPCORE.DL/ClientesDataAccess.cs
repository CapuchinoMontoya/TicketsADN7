using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar el acceso a las ventas
    /// </summary>
    public class ClientesDataAccess
    {
        #region Consultar

        /// <summary>
        /// Ejecutar el SP para obtener los clientes perdidos por agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<ClientesPerdidos> ObtenerClientePerdidoXAgente(VentaXAgente model)
        {
            try
            {
                var storeProcedureParameters = new List<StoreProcedureParameters>();

                foreach (var property in model.GetType().GetProperties())
                {
                    var paramName = "@" + property.Name;
                    var paramValue = property.GetValue(model) ?? DBNull.Value;

                    storeProcedureParameters.Add(new StoreProcedureParameters
                    {
                        ParamName = paramName,
                        ParamValue = paramValue
                    });
                }

                var result = CommonExecuteStoreProcedure<ClientesPerdidos>.EjecutarStoreProcedure("sp_Portal_ClientesNoCompraron", storeProcedureParameters, true, oneRowResponse: false);

                return result.ObjectData ?? new List<ClientesPerdidos>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerMovAgente, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }
        #endregion Consultar
    }
}