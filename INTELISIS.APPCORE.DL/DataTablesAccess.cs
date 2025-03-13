using ClosedXML.Excel;
using System.Data;

namespace INTELISIS.APPCORE.DL
{
    /// <summary>
    /// Clase para modificar, crear o utilizar datatables genericos
    /// </summary>
    public class DataTablesAccess
    {
        #region Metodos

        /// <summary>
        /// Convertir modelo o lista a DataSet
        /// </summary>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            try
            {
                DataTable table = new DataTable();

                // Obtener las propiedades del modelo
                var propiedades = typeof(T).GetProperties();

                // Agregar columnas al DataTable basadas en las propiedades del modelo
                foreach (var prop in propiedades)
                {
                    // Verificar si el tipo es Nullable<T>
                    Type columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    // Agregar la columna al DataTable con el tipo correcto
                    table.Columns.Add(prop.Name, columnType);
                }

                // Agregar filas al DataTable
                foreach (var item in data)
                {
                    var fila = table.NewRow();
                    foreach (var prop in propiedades)
                    {
                        fila[prop.Name] = prop.GetValue(item) ?? DBNull.Value; // Manejar valores nulos
                    }
                    table.Rows.Add(fila);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Error capa de datos (public static DataTable ConvertToDataTable<T>(List<T> data)): " + ex.Message);
            }
        }

        /// <summary>
        /// Convertir DataSet a Excel
        /// </summary>
        /// <returns></returns>
        public static byte[] ExportToExcel(DataTable table, string nombre)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    // Agregar una hoja al libro de Excel
                    var worksheet = workbook.Worksheets.Add(table, nombre);

                    // 1. Estilo para el título
                    var titleCell = worksheet.Cell(1, 1).SetValue("Reporte Profesional"); // Título del reporte
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.Font.FontSize = 16;
                    titleCell.Style.Font.FontColor = XLColor.White;
                    titleCell.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                    titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range(1, 1, 1, table.Columns.Count).Merge(); // Combinar celdas para el título

                    // 2. Estilo para los encabezados de la tabla
                    var headerRow = worksheet.Row(2);
                    headerRow.Style.Font.Bold = true;
                    headerRow.Style.Font.FontColor = XLColor.White;
                    headerRow.Style.Fill.BackgroundColor = XLColor.Blue;
                    headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerRow.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    headerRow.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // 3. Estilo para las celdas de datos
                    var dataRange = worksheet.Range(3, 1, table.Rows.Count + 2, table.Columns.Count);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    dataRange.Style.Font.FontColor = XLColor.Black;

                    // 4. Formato condicional para resaltar valores importantes
                    var conditionalRange = worksheet.Range(3, 1, table.Rows.Count + 2, table.Columns.Count);
                    conditionalRange.AddConditionalFormat().WhenGreaterThan(100).Fill.SetBackgroundColor(XLColor.LightGreen); // Resaltar valores mayores a 100
                    conditionalRange.AddConditionalFormat().WhenLessThan(0).Fill.SetBackgroundColor(XLColor.LightCoral); // Resaltar valores negativos

                    // 5. Formato de números y fechas
                    foreach (var column in worksheet.Columns())
                    {
                        if (column.Cell(3).DataType == XLDataType.DateTime)
                        {
                            column.Style.DateFormat.Format = "dd/MM/yyyy"; // Formato de fecha
                        }
                        else if (column.Cell(3).DataType == XLDataType.Number)
                        {
                            column.Style.NumberFormat.Format = "#,##0.00"; // Formato de números con separador de miles
                        }
                    }

                    // 6. Ajustar el ancho de las columnas automáticamente
                    worksheet.Columns().AdjustToContents();

                    // 7. Congelar la fila de encabezados
                    worksheet.SheetView.Freeze(2, 0); // Congelar la segunda fila (encabezados)

                    // 8. Agregar un pie de página (opcional)
                    var footerRow = worksheet.Row(table.Rows.Count + 3);
                    footerRow.Cell(1).SetValue($"Generado el: {DateTime.Now.ToShortDateString()}");
                    footerRow.Style.Font.Italic = true;
                    footerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // 9. Guardar el libro en un MemoryStream
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray(); // Devolver el archivo como byte[]
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el archivo Excel: " + ex.Message);
            }
        }
        #endregion Insertar
    }
}