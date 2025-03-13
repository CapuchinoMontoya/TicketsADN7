using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de las ventas
    /// </summary>
    public class VentaXAgenteBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtiene la venta por Agente con filtros
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerVentaXAgente(VentaXAgente model)
        {
            return VentaXAgenteDataAccess.ObtenerVentaXAgente(model);
        }
        /// <summary>
        /// Método que obtien el avenace de venta mensual
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerAvanceMensualXAgente(VentaXAgente model)
        {
            return VentaXAgenteDataAccess.ObtenerAvanceMensualXAgente(model);
        }
        /// <summary>
        /// Método que obtiene los ultimos movimientos por agente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<MovimientosVentas> ObtenerMovAgente(VentaXAgente model)
        {
            return VentaXAgenteDataAccess.ObtenerMovAgente(model);
        }
        #endregion Acción
    }
}