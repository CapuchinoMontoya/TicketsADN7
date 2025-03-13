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
    public class VentaXAgenteDataAccess
    {
        #region Consultar

        /// <summary>
        /// Ejecutar el SP para obtener la venta por Agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerVentaXAgente(VentaXAgente model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_VentaXAgente", storeProcedureParameters, false);
                double resultVenta = Convert.ToDouble(result.ObjectData);

                return resultVenta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerVentaXAgente, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener la venta por Agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerAvanceMensualXAgente(VentaXAgente model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_AvanceVentasXAgente", storeProcedureParameters, false);
                double resultVenta = Convert.ToDouble(result.ObjectData);

                return resultVenta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerAvanceMensualXAgente, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener los movimientos por Agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<MovimientosVentas> ObtenerMovAgente(VentaXAgente model)
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

                var result = CommonExecuteStoreProcedure<MovimientosVentas>.EjecutarStoreProcedure("sp_Portal_UltimosMovAgente", storeProcedureParameters, true, oneRowResponse: false);

                return result.ObjectData ?? new List<MovimientosVentas>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerMovAgente, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }
        #endregion Consultar
    }
}