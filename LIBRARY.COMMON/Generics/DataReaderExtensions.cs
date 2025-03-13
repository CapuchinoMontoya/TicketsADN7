using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace LIBRARY.COMMON.Generics
{
    /// <summary>
    /// Clase de extensiones para SqlDataReader que proporciona métodos útiles para trabajar con DataReaders.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Convierte un SqlDataReader en una lista de objetos de tipo T.
        /// </summary>
        /// <typeparam name="T">El tipo de objeto al que se convertirá el DataReader.</typeparam>
        /// <param name="reader">El SqlDataReader que contiene los datos a convertir.</param>
        /// <param name="factory">Función que crea una instancia de T.</param>
        /// <returns>Una lista de objetos de tipo T.</returns>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la conversión.</exception>
        public static List<T> ToList<T>(SqlDataReader reader, Func<T> factory) where T : new()
        {
            var resultList = new List<T>(); // Lista que almacenará los objetos convertidos.
            var properties = typeof(T).GetProperties(); // Obtiene las propiedades públicas de T.

            try
            {
                if (reader.HasRows) // Verifica si el DataReader tiene filas.
                {
                    while (reader.Read()) // Itera sobre cada fila del DataReader.
                    {
                        T model = factory(); // Crea una instancia de T usando la función factory.

                        foreach (PropertyInfo property in properties) // Itera sobre las propiedades de T.
                        {
                            var columnName = property.Name; // Obtiene el nombre de la propiedad.

                            if (reader.HasColumn(columnName)) // Verifica si la columna existe en el DataReader.
                            {
                                var value = reader[columnName]; // Obtiene el valor de la columna.

                                if (value != DBNull.Value) // Verifica si el valor no es DBNull.
                                {
                                    // Convierte el valor al tipo de la propiedad (manejando tipos Nullable).
                                    Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                    object convertedValue = Convert.ChangeType(value, propertyType);

                                    // Asigna el valor convertido a la propiedad del objeto.
                                    property.SetValue(model, convertedValue);
                                }
                            }
                        }

                        resultList.Add(model); // Agrega el objeto a la lista.
                    }
                }

                return resultList; // Retorna la lista de objetos.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException); // Lanza una excepción si ocurre un error.
            }
        }

        /// <summary>
        /// Verifica si una columna con el nombre especificado existe en el SqlDataReader.
        /// </summary>
        /// <param name="reader">El SqlDataReader en el que se buscará la columna.</param>
        /// <param name="columnName">El nombre de la columna a buscar.</param>
        /// <returns>True si la columna existe; de lo contrario, false.</returns>
        private static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++) // Itera sobre las columnas del DataReader.
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase)) // Compara los nombres de las columnas.
                    return true; // Retorna true si encuentra la columna.
            }

            return false; // Retorna false si no encuentra la columna.
        }
    }
}