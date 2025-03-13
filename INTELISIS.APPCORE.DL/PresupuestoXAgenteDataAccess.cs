using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar el acceso a la tabla Art de la base de datos
    /// </summary>
    public class PresupuestoXAgenteDataAccess
    {
        #region Consultar

        /// <summary>
        /// Ejecutar el SP para obtener el presupuesto por Agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerPresupuestoXAgente(PresupuestoXAgente model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_PresupuestoMensualXAgente", storeProcedureParameters, false);
                double resultPRESUPUESTO = Convert.ToDouble(result.ObjectData);

                return resultPRESUPUESTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerPresupuestoXAgente, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        #endregion Consultar
    }
}