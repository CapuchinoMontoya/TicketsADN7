using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de los tipos de objetos Art
    /// </summary>
    public class SPIDBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtiene todos los objetos de Art en una lista de objetos Art --> List<Art>
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public static async Task<int> ObtenerSPID()
        {
            return await SPIDDataAccess.ObtenerSPID();
        }

        #endregion Acción
    }
}