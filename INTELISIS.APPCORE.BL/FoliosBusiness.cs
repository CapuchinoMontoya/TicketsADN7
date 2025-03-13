using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de los tipos de objetos Art
    /// </summary>
    public class FoliosBusiness
    {
        #region Insertar

        /// <summary>
        /// Método que inserta movimiento (Pedidos, Cotizacions, Presupuestos, etc) y retorna el folio
        /// </summary>
        /// <returns></returns>
        public static async Task<string> InsertarMov(string SPID, string insertConDatos)
        {
            return await FoliosDataAccess.InsertarMov(SPID, insertConDatos);
        }

        #endregion Acción
    }
}