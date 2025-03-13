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
    public class PresupuestoXAgenteBusiness
    {
        #region Consultar

        /// <summary>
        /// Método que obtieneel presupuesto por Agente con filtros
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double ObtenerPresupuestoXAgente(PresupuestoXAgente model)
        {
            return PresupuestoXAgenteDataAccess.ObtenerPresupuestoXAgente(model);
        }

        #endregion Acción
    }
}