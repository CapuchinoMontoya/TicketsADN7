using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace LIBRARY.COMMON.Generics
{
    /// <summary>
    /// Clase genérica que proporciona métodos para convertir entre DataReaders, listas de objetos y DataTables.
    /// </summary>
    /// <typeparam name="T">El tipo de objeto con el que se trabajará.</typeparam>
    public class LibraryGenerics<T>
    {
        /// <summary>
        /// Convierte un IDataReader en una lista de objetos de tipo T.
        /// </summary>
        /// <param name="data">El IDataReader que contiene los datos a convertir.</param>
        /// <returns>Una lista de objetos de tipo T.</returns>
        public static List<T> ConvertDataSetToList(IDataReader data)
        {
            List<T> lista = new List<T>();
            try
            {
                if (data != null)
                {
                    while (data.Read())
                    {
                        T itemClass = ConvertReaderToObject(data);
                        lista.Add(itemClass);
                    }
                }
            }
            finally
            {
                data.Close();
            }
            return lista;
        }

        /// <summary>
        /// Convierte una fila de un IDataReader en un objeto de tipo T.
        /// </summary>
        /// <param name="data">El IDataReader que contiene la fila de datos a convertir.</param>
        /// <returns>Un objeto de tipo T.</returns>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la conversión.</exception>
        public static T ConvertReaderToObject(IDataReader data)
        {
            try
            {
                T itemClass;
                if (typeof(T) == typeof(string))
                {
                    itemClass = (T)(object)data[0].ToString();
                }
                else
                {
                    itemClass = (T)Activator.CreateInstance(typeof(T));
                    PropertyInfo[] properties = itemClass.GetType().GetProperties((Recursos.flags));

                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        string currentName = data.GetName(i);
                        PropertyInfo currentProperty = properties.FirstOrDefault(
                            x => currentName.ToLower().Equals(x.Name.ToLower()));

                        if (currentProperty != null)
                        {
                            if (data[currentName] != null && !DBNull.Value.Equals(data[currentName]))
                                currentProperty.SetValue(itemClass, data[currentName], null);
                        }
                    }
                }
                return itemClass;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Convierte una lista de objetos de tipo T en un DataTable.
        /// </summary>
        /// <typeparam name="T">El tipo de objeto en la lista.</typeparam>
        /// <param name="items">La lista de objetos a convertir.</param>
        /// <returns>Un DataTable con los datos de la lista.</returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? Nullable.GetUnderlyingType(prop.PropertyType)
                    : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}