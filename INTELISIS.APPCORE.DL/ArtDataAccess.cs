using LIBRARY.COMMON.Generics;
using System.Data.SqlClient;
using System.Data;
using INTELISIS.APPCORE.EL;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para controlar el acceso a la tabla Art de la base de datos
    /// </summary>
    public class ArtDataAccess
    {
        #region Consultar

        /// <summary>
        /// Obtener todos los registros de Art
        /// </summary>
        /// <returns></returns>
        public static List<Art> ObtenerArt()
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandType = CommandType.StoredProcedure;
                sqlcComando.CommandText = "web_spS_ObtenerArt";

                //2. Declarar los parametros

                //3. Agregar los parametros al comando

                //4. Abrir la conexión
                sqlcComando.Connection.Open();

                //5. Ejecutar la instrucción SELECT que regresa filas
                SqlDataReader reader = sqlcComando.ExecuteReader();

                //6. Asignar la lista de Clientes
                List<Art> result = LibraryGenerics<Art>.ConvertDataSetToList(reader);

                //7. Cerrar la conexión
                sqlcComando.Connection.Close();

                //8. Regresar el resultado
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static List<Art> ObtenerArt()): " + ex.Message);
            }
        }
        /// <summary>
        /// Obtener los articulos con su descuento
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public static async Task<List<ArticuloConDescuento>> ObtenerArticulosConDescuento(string cliente)
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandType = CommandType.StoredProcedure;
                sqlcComando.CommandText = "spPORTAL_ObtenerArticulosConDescuentoTests";

                //2. Declarar los parametros
                SqlParameter sqlpArticuloDescripcion = new SqlParameter();
                sqlpArticuloDescripcion.ParameterName = "@Cliente";
                sqlpArticuloDescripcion.SqlDbType = SqlDbType.VarChar;
                sqlpArticuloDescripcion.Value = cliente;

                //3. Agregar los parametros al comando
                sqlcComando.Parameters.Add(sqlpArticuloDescripcion);

                //4. Abrir la conexión
                sqlcComando.Connection.Open();

                //5. Ejecutar la instrucción SELECT que regresa filas
                SqlDataReader reader = sqlcComando.ExecuteReader();

                //6. Asignar la lista de Clientes
                List<ArticuloConDescuento> result = LibraryGenerics<ArticuloConDescuento>.ConvertDataSetToList(reader);

                //7. Cerrar la conexión
                sqlcComando.Connection.Close();

                //8. Regresar el resultado
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static List<Art> ObtenerArticulosConDescuento(string " + cliente + ")): " + ex.Message);
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener el articulo por codigo de barras
        /// </summary>
        /// <param name="codigoBarras"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ObtenerArticuloConCodigoDeBarras(string codigoBarras)
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandType = CommandType.StoredProcedure;
                sqlcComando.CommandText = "sp_PuntoVenta_ArtCodigoBarras";

                //2. Declarar los parametros
                SqlParameter sqlpArticuloDescripcion = new SqlParameter();
                sqlpArticuloDescripcion.ParameterName = "@Codigo";
                sqlpArticuloDescripcion.SqlDbType = SqlDbType.VarChar;
                sqlpArticuloDescripcion.Value = codigoBarras;

                //3. Agregar los parametros al comando
                sqlcComando.Parameters.Add(sqlpArticuloDescripcion);

                //4. Abrir la conexión
                sqlcComando.Connection.Open();

                // 5. Ejecutar la instrucción SELECT que regresa filas
                using (SqlDataReader reader = sqlcComando.ExecuteReader())
                {
                    // 6. Verificar si hay filas y leer el artículo
                    if (reader.HasRows)
                    {
                        reader.Read(); // Leer la primera fila
                        string articulo = reader["Articulo"].ToString(); // Obtener el valor de la columna "Articulo"
                        return articulo; // Retornar el artículo
                    }
                    else
                    {
                        return null; // Si no hay filas, retornar null
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static List<Art> ObtenerArticulosConDescuento(string " + codigoBarras + ")): " + ex.Message);
            }
        }

        /// <summary>
        /// Ejecutar el SP para obtener los precios con impuestos de un articulo
        /// </summary>
        /// <param name="articulo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<PrecioArtCodigoBarras>> ObtenerPrecioConCodigoDeBarras(string articulo, string listaPrecios)
        {
            try
            {
                //1. Configurar la conexión y el tipo de comando
                SqlConnection sqlcConectar = new SqlConnection(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new SqlCommand();
                sqlcComando.Connection = sqlcConectar;
                sqlcComando.CommandType = CommandType.StoredProcedure;
                sqlcComando.CommandText = "sp_PuntoVenta_CalcularPrecioTotal";

                //2. Declarar los parametros
                SqlParameter sqlpListaPrecios = new SqlParameter();
                sqlpListaPrecios.ParameterName = "@Lista";
                sqlpListaPrecios.SqlDbType = SqlDbType.VarChar;
                sqlpListaPrecios.Value = listaPrecios;

                SqlParameter sqlpArticulo = new SqlParameter();
                sqlpArticulo.ParameterName = "@Articulo";
                sqlpArticulo.SqlDbType = SqlDbType.VarChar;
                sqlpArticulo.Value = articulo;

                //3. Agregar los parametros al comando
                sqlcComando.Parameters.Add(sqlpListaPrecios);
                sqlcComando.Parameters.Add(sqlpArticulo);

                //4. Abrir la conexión
                sqlcComando.Connection.Open();

                //5. Ejecutar la instrucción SELECT que regresa filas
                SqlDataReader reader = sqlcComando.ExecuteReader();

                //6. Asignar la lista de Clientes
                List<PrecioArtCodigoBarras> result = LibraryGenerics<PrecioArtCodigoBarras>.ConvertDataSetToList(reader);

                //7. Cerrar la conexión
                sqlcComando.Connection.Close();

                //8. Regresar el resultado
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static List<Art> ObtenerPreciosConImpuestosXArticulo(string " + articulo + ")): " + ex.Message);
            }
        }
        #endregion Consultar
    }
}