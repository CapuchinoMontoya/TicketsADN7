using System.Reflection;
using System;

namespace INTELISIS.APPCORE.EL.Enums
{
    public static class StoredProceduresEnums
    {
        public static readonly string TM_OP_LevantamientoTarjetasCaseta = "SpWeb_TM_Operaciones_LevantamientoTarjetasCaseta";
        public static readonly string TM_OP_TarjetasPendientesConcluir = "TM_Operaciones_TarjetasPendienteDeConcluir";
        public static readonly string TM_OP_DesempenioRutaZona = "TM_Operaciones_PorcentajeDesempenioRutaZona";
        public static readonly string TM_OP_RutasCargadasAlDiaAnterior = "TM_Operaciones_RutasCargadosDiaAnterior";
        public static readonly string TM_OP_DesempenioPorZona = "TM_Operaciones_PorcentajeDesempenioZona";
        public static readonly string TM_OP_SKUsPreparadosMontacarguistas = "SpWeb_TM_Operaciones_SKUsPreparadosPorMontacarguista";
        public static readonly string TM_OP_SKUsPorMotivosDevolucion = "TM_Operaciones_CantDeSkusPorMotivoDevolucion";
        public static readonly string TM_OP_OrdenesTraspasoPendientes = "SpWeb_TM_Operaciones_OrdenesTraspasoPendientes";
        public static readonly string TM_OP_KilogramosPorRuta = "TM_Operaciones_KgPorRuta";
        public static readonly string TM_OP_CantidadSKUsSinExistencias = "TM_Operaciones_CantSkusSinExistencias";
        public static readonly string TM_OP_Desembarques = "WebAppDesasignacionesYDesembarques";
        public static readonly string TM_OP_SKUsNoSurtidosPorNoExistencias = "TC_SKU_Sin_Surtir_No_Hay_Mat_Fisicamente";
        public static readonly string TM_OP_SeguimientoDesembarques = "WebAppSeguimientoDesembarques";
        public static readonly string TM_OP_PreparacionesDeSKUsyUnidades = "TM_Operaciones_PreparacionesSkuUnidades";
        public static readonly string TM_OP_OrdenesTraspasoParaFacturas = "TM_Operaciones_OTSurtirFacturas";
        public static readonly string TM_LOG_DesempenioRutasPorZonas = "TC_Logistica_PorcentajeCapacidadUtilizadaRutaZona";
        public static readonly string R_Web_CheckListIng = "SP_ReportesWEB_Segundaschecklist2";
        public static readonly string R_Web_HistoricoComprasCliente = "SpWeb_ReportesWeb_HitoricoComprasCliente";
        public static readonly string R_Web_TransferenciasRotoPorChofer = "SpR_Web_AsociarTransfrenciasPorChofer";
        public static readonly string R_Web_EntradasCompraMontacarguistas = "Web_Sp_TableroMandoMontacarguistas";

        public static string GetStoredProcedure(string key)
        {
            var field = typeof(StoredProceduresEnums).GetField(key, BindingFlags.Static | BindingFlags.Public);
            if (field != null)
            {
                return (string)field.GetValue(null);
            }
            throw new ArgumentException($"No se ha encontrado la instrucción solicitada para {key}");
        }
    }
}
