using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para insertar Movimientos en la BD e Intelsis
    /// </summary>
    public class FoliosDataAccess
    {
        #region Insertar

        /// <summary>
        /// Obtener todos los registros de Art
        /// </summary>
        /// <returns></returns>
        public static async Task<string> InsertarMov(string SPID, string insertConDatos)
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandType = CommandType.StoredProcedure;
                sqlcComando.CommandText = "spPortal_ExecuteDynamicSql";

                //2. Declarar los parametros
                SqlParameter sqlpSPID = new SqlParameter();
                sqlpSPID.ParameterName = "@spid";
                sqlpSPID.SqlDbType = SqlDbType.VarChar;
                sqlpSPID.Value = SPID;

                SqlParameter sqlpInsertConDatos = new SqlParameter();
                sqlpInsertConDatos.ParameterName = "@InsertDataScript";
                sqlpInsertConDatos.SqlDbType = SqlDbType.VarChar;
                sqlpInsertConDatos.Value = insertConDatos;

                //3. Agregar los parametros al comando
                sqlcComando.Parameters.Add(sqlpSPID);
                sqlcComando.Parameters.Add(sqlpInsertConDatos);

                //4. Abrir la conexión
                sqlcComando.Connection.Open();

                //5. Ejecutar la instrucción SELECT que regresa filas
                var reader = await sqlcComando.ExecuteScalarAsync();

                //6. Asignar el valor
                string? result = reader?.ToString();

                //7. Cerrar la conexión
                sqlcComando.Connection.Close();

                //8. Regresar el resultado
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static async Task<string> InsertarMov(string SPID, string insertConDatos)): " + ex.Message);
            }
        }

        #endregion Insertar
    }
}