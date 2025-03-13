using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar el acceso al SPID
    /// </summary>
    public class SPIDDataAccess
    {
        #region Consultar

        /// <summary>
        /// Obtener el SPID
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ObtenerSPID()
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandText = "SELECT @@SPID";

                //2. Abrir la conexión
                sqlcComando.Connection.Open();

                //3. Ejecutar SELECT
                var result = await sqlcComando.ExecuteScalarAsync();
                var spidValue = Convert.ToInt32(result);

                //4. Cerrar la conexión
                sqlcComando.Connection.Close();

                //5. Retornar valor
                return spidValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static async Task<int> ObtenerSPIDAsync()): " + ex.Message);
            }
        }
        #endregion Consultar
    }
}