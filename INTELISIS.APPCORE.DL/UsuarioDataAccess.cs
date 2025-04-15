using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar las acciones de los usuarios
    /// </summary>
    public class UsuarioDataAccess
    {
        #region Consultar

        /// <summary>
        /// Ejecutar el SP para obtener la respuesta del Inicio de Sesion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Usuario IniciarSesion(LoginModel model)
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

                var result = CommonExecuteStoreProcedure<Usuario>.EjecutarStoreProcedure("sp_Tickets_Login", storeProcedureParameters, true, oneRowResponse: true);

                return result.ObjectData ?? new Usuario();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de datos IniciarSesion, debido a {ex.Message} {ex.InnerException?.Message ?? ""}");
            }
        }
        #endregion Consultar
    }
}