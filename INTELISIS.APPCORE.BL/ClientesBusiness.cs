using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra a los clientes
    /// </summary>
    public class ClientesBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtiene los clientes perdidos por agente y mes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ClientesPerdidos> ObtenerClientePerdidoXAgente(VentaXAgente model)
        {
            return ClientesDataAccess.ObtenerClientePerdidoXAgente(model);
        }

        #endregion Acción
    }
}