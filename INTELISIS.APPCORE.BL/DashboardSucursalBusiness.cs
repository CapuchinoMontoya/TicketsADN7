using System.Collections.Generic;
using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;
using System;
using System.Drawing;
using System.IO;
using Art = INTELISIS.APPCORE.EL.Art;
using Cte = INTELISIS.APPCORE.EL.Cte;
using static System.Net.Mime.MediaTypeNames;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los cálculos y operaciones de los tipos de objetos Art
    /// </summary>
    public class DashboardSucursalBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtiene el presupuesto por Sucursal con filtros
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerPresupuestoXSucursal(PresupuestoXSucursal model)
        {
            return DashboardSucursalDataAccess.ObtenerPresupuestoXSucursal(model);
        }

        /// <summary>
        /// Método que obtiene el venta por Sucursal con filtros
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerVentaXSucursal(VentaXSucursal model)
        {
            return DashboardSucursalDataAccess.ObtenerVentaXSucursal(model);
        }

        /// <summary>
        /// Método que obtiene el avance por Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerAvanceXSucursal(VentaXSucursal model)
        {
            return DashboardSucursalDataAccess.ObtenerAvanceXSucursal(model);
        }

        /// <summary>
        /// Método que obtiene el general por mes de la Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<GeneralSucursal> ObtenerGeneralXMes(GeneralXSucursal model)
        {
            return DashboardSucursalDataAccess.ObtenerGeneralXMes(model);
        }

        /// <summary>
        /// Método que obtiene el general por trimestre de la Sucursal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<GeneralSucursal> ObtenerGeneralXTrimestre(GeneralXSucursal model)
        {
            return DashboardSucursalDataAccess.ObtenerGeneralXTrimestre(model);
        }

        #endregion Acción
    }
}