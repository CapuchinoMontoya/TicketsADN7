using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de los tipos de objetos Art
    /// </summary>
    public class ArtBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtiene todos los objetos de Art en una lista de objetos Art --> List<Art>
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public static async Task<List<ArticuloConDescuento>> ObtenerArticulosConDescuento(string cliente)
        {
            return await ArtDataAccess.ObtenerArticulosConDescuento(cliente);
        }

        /// <summary>
        /// Método que obtiene el articulo por codigo de barras
        /// </summary>
        /// <param name="codigoBarras"></param>
        /// <returns></returns>
        public static string ObtenerArticuloConCodigoDeBarras(string codigoBarras)
        {
            return ArtDataAccess.ObtenerArticuloConCodigoDeBarras(codigoBarras);
        }

        /// <summary>
        /// Método que obtiene el precio del articulo con codigo de barras
        /// </summary>
        /// <param name="articulo"></param>
        /// <returns></returns>
        public static async Task<List<PrecioArtCodigoBarras>> ObtenerPrecioConCodigoDeBarras(string articulo, string listaPrecios)
        {
            return await ArtDataAccess.ObtenerPrecioConCodigoDeBarras(articulo, listaPrecios);
        }

        #endregion Acción
    }
}