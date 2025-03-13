using INTELISIS.APPCORE.EL;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar el acceso a la tabla Art de la base de datos
    /// </summary>
    public class DashboardSucursalDataAccess
    {
        #region Consultar

        /// <summary>
        /// Ejecutar el SP para obtener el presupuesto por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerPresupuestoXSucursal(PresupuestoXSucursal model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_PresupuestoMensualXSucursal", storeProcedureParameters, false);
                double resultPRESUPUESTO = Convert.ToDouble(result.ObjectData);

                return resultPRESUPUESTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerPresupuestoXSucursal, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }


        /// <summary>
        /// Ejecutar el SP para obtener la venta por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerVentaXSucursal(VentaXSucursal model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_VentaXSucursal", storeProcedureParameters, false);
                double resultPRESUPUESTO = Convert.ToDouble(result.ObjectData);

                return resultPRESUPUESTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerVentaXSucursal, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener el avance por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ObtenerAvanceXSucursal(VentaXSucursal model)
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

                var result = CommonExecuteStoreProcedure<int>.EjecutarStoreProcedure("sp_Portal_AvanceVentasXSucursal", storeProcedureParameters, false);
                double resultPRESUPUESTO = Convert.ToDouble(result.ObjectData);

                return resultPRESUPUESTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerAvanceXSucursal, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener el general por mes por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<GeneralSucursal> ObtenerGeneralXMes(GeneralXSucursal model)
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
                var result = CommonExecuteStoreProcedure<GeneralSucursal>.EjecutarStoreProcedure("sp_Portal_GeneralSucursal", storeProcedureParameters, true, oneRowResponse: false);
                return result.ObjectData ?? new List<GeneralSucursal>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerGeneralXMes, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener el general por trimestre por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<GeneralSucursal> ObtenerGeneralXTrimestre(GeneralXSucursal model)
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

                var result = CommonExecuteStoreProcedure<GeneralSucursal>.EjecutarStoreProcedure("sp_Portal_GeneralSucursal", storeProcedureParameters, true, oneRowResponse: false);
                return result.ObjectData ?? new List<GeneralSucursal>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos ObtenerGeneralXTrimestre, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }
        #endregion Consultar
    }
}