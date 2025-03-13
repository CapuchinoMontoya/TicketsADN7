using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using INTELISIS.APPCORE.EL;
using LIBRARY.COMMON.Generics;
using INTELISIS.APPCORE.EL.Enums;

namespace INTELISIS.APPCORE.DL
{
    public class CommonExecuteStoreProcedure<T>
    {
        /// <summary>
        /// Metodo para ejecuar procedimientos almacenados de forma dinamica por medio del nombre
        /// </summary>
        /// <param name="nombreSP">Nombre del procedimiento almacenado</param>
        /// <param name="regresaDatos">Bandera para hacer la lectura de la respuesta o no</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ResponseData EjecutarStoreProcedure(string nombreSP, List<StoreProcedureParameters> spParams, bool regresaDatos, bool getIdentityValue = false, bool oneRowResponse = false)
        {
            try
            {
                ResponseData responseData = new();
                SqlConnection sqlcConectar = new(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new()
                {
                    Connection = sqlcConectar,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 180,
                    CommandText = nombreSP
                };

                /* Obteniendo la lista de parametros del SP de la base de datos */
                List<StoreProcedureParameters> paramsListFromSQL = GetStoreProcedureParams(nombreSP);

                foreach (StoreProcedureParameters p in spParams)
                {
                    var currentParam = paramsListFromSQL.FirstOrDefault(x => x.ParamName == p.ParamName) ?? throw new Exception($"El parametro: '{p.ParamName}' no existe para el SP: '{nombreSP}'.");
                    sqlcComando.Parameters.Add(new SqlParameter
                    {
                        ParameterName = p.ParamName,
                        SqlDbType = currentParam.SqlDbType,
                        Value = p.ParamValue
                    });
                }

                /* Almacenando el resultado en vaiable dinamica */
                sqlcComando.Connection.Open();
                if (regresaDatos)
                {
                    SqlDataReader reader = sqlcComando.ExecuteReader();
                    if (oneRowResponse)
                        responseData.ObjectData = LibraryGenerics<T>.ConvertDataSetToList(reader).FirstOrDefault();
                    else
                        responseData.ObjectData = LibraryGenerics<T>.ConvertDataSetToList(reader);
                }
                else if (getIdentityValue)
                {
                    SqlDataReader reader = sqlcComando.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            responseData.ObjectData = reader[0] == DBNull.Value ? 0 : !string.IsNullOrEmpty(reader[0].ToString()) ? Convert.ToInt32(reader[0].ToString()) : 0;
                }
                else responseData.ObjectData = sqlcComando.ExecuteScalar();

                /* Cerrando la conexion */
                sqlcComando.Connection.Close();

                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static List<dynamic> EjecutarStoreProcedureDinamico(string nombreSP, List<StoreProcedureParameters> spParams)
        {
            try
            {
                // Validando que el Store Procedure Se pueda ejecutar
                nombreSP = StoredProceduresEnums.GetStoredProcedure(nombreSP);

                SqlConnection sqlcConectar = new(AppSettings.DatosConexion);

                SqlCommand sqlcComando = new()
                {
                    Connection = sqlcConectar,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 180,
                    CommandText = nombreSP
                };

                /* Obteniendo la lista de parametros del SP de la base de datos */
                List<StoreProcedureParameters> paramsListFromSQL = GetStoreProcedureParams(nombreSP);

                foreach (StoreProcedureParameters p in spParams)
                {
                    var currentParam = paramsListFromSQL.FirstOrDefault(x => x.ParamName == p.ParamName)
                                        ?? throw new Exception($"El parametro: '{p.ParamName}' no existe para el SP: '{nombreSP}'.");
                    sqlcComando.Parameters.Add(new SqlParameter
                    {
                        ParameterName = p.ParamName,
                        SqlDbType = currentParam.SqlDbType,
                        Value = p.ParamValue
                    });
                }

                /* Almacenando el resultado en variable dinamica */
                sqlcComando.Connection.Open();

                List<dynamic> resultList = [];
                using (SqlDataReader reader = sqlcComando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic expandoObject = new ExpandoObject();
                        var dictionary = (IDictionary<string, object>)expandoObject;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            dictionary.Add(reader.GetName(i), value);
                        }

                        resultList.Add(expandoObject);
                    }
                }

                /* Cerrando la conexion */
                sqlcComando.Connection.Close();

                return resultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Metodo para obtener el listado de los parametros reuqeridos por el SP
        /// con las propiedades de cada uno
        /// </summary>
        /// <param name="sqlcComando">Conexion abierta de SQL</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<StoreProcedureParameters> GetStoreProcedureParams(string nombreSP)
        {
            try
            {
                SqlConnection sqlcConectar = new(AppSettings.DatosConexion);
                SqlCommand sqlcComando = new()
                {
                    Connection = sqlcConectar,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = nombreSP
                };

                sqlcComando.Connection.Open();

                List<StoreProcedureParameters> paramsList = new();

                SqlCommandBuilder.DeriveParameters(sqlcComando);

                foreach (SqlParameter p in sqlcComando.Parameters)
                {
                    paramsList.Add(new StoreProcedureParameters
                    {
                        ParamName = p.ParameterName,
                        SqlDbType = p.SqlDbType
                    });
                }

                sqlcComando.Connection.Close();

                return paramsList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}