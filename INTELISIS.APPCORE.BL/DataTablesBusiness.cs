using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;
using System.Data;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de los tipos de objetos Art
    /// </summary>
    public class DataTablesBusiness
    {
        #region Metodos

        /// <summary>
        /// Método que convierte los modelos o listas a dataset
        /// </summary>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            return DataTablesAccess.ConvertToDataTable(data);
        }

        /// <summary>
        /// Método que convierte los dataset en Excel
        /// </summary>
        /// <returns></returns>
        public static byte[] ExportToExcel(DataTable table, string nombre)
        {
            return DataTablesAccess.ExportToExcel(table, nombre);
        }

        #endregion Acción
    }
}