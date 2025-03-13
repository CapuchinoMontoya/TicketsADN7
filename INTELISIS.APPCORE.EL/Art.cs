using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Art
    {
        public string Articulo { get; set; } = null!;

        public string? Rama { get; set; }

        public string? Descripcion1 { get; set; }

        public string? Descripcion2 { get; set; }

        public string? NombreCorto { get; set; }

        public string? Grupo { get; set; }

        public string? Categoria { get; set; }

        public string? CategoriaActivoFijo { get; set; }

        public string? Familia { get; set; }

        public string? Linea { get; set; }

        public string? Fabricante { get; set; }

        public string? ClaveFabricante { get; set; }

        public double Impuesto1 { get; set; }

        public double? Impuesto2 { get; set; }

        public double? Impuesto3 { get; set; }

        public string? Factor { get; set; }

        public string? Unidad { get; set; }

        public string? UnidadCompra { get; set; }

        public string? UnidadTraspaso { get; set; }

        public double? UnidadCantidad { get; set; }

        public string? TipoCosteo { get; set; }

        public double? Peso { get; set; }

        public double? Tara { get; set; }

        public double? Volumen { get; set; }

        public string Tipo { get; set; } = null!;

        public string TipoOpcion { get; set; } = null!;

        public bool Accesorios { get; set; }

        public bool Refacciones { get; set; }

        public bool Sustitutos { get; set; }

        public bool? Servicios { get; set; }

        public bool? Consumibles { get; set; }

        public string MonedaCosto { get; set; } = null!;

        public string MonedaPrecio { get; set; } = null!;

        public decimal? MargenMinimo { get; set; }

        public decimal? PrecioLista { get; set; }

        public decimal? PrecioMinimo { get; set; }

        public double? FactorAlterno { get; set; }

        public decimal? PrecioAnterior { get; set; }

        public string? Utilidad { get; set; }

        public double? DescuentoCompra { get; set; }

        public string? Clase { get; set; }

        public string Estatus { get; set; } = null!;

        public DateTime? UltimoCambio { get; set; }

        public DateTime? Alta { get; set; }

        public bool Conciliar { get; set; }

        public string? Mensaje { get; set; }

        public string? Comision { get; set; }

        public string? Arancel { get; set; }

        public string? ArancelDesperdicio { get; set; }

        public string? Abc { get; set; }

        public string? Usuario { get; set; }

        public decimal? Precio2 { get; set; }

        public decimal? Precio3 { get; set; }

        public decimal? Precio4 { get; set; }

        public decimal? Precio5 { get; set; }

        public decimal? Precio6 { get; set; }

        public decimal? Precio7 { get; set; }

        public decimal? Precio8 { get; set; }

        public decimal? Precio9 { get; set; }

        public decimal? Precio10 { get; set; }

        public bool Refrigeracion { get; set; }

        public bool TieneCaducidad { get; set; }

        public bool? BasculaPesar { get; set; }

        public bool SeProduce { get; set; }

        public string? Situacion { get; set; }

        public DateTime? SituacionFecha { get; set; }

        public string? SituacionUsuario { get; set; }

        public string? SituacionNota { get; set; }

        public string? EstatusPrecio { get; set; }

        public bool? WMostrar { get; set; }

        public double? Merma { get; set; }

        public double? Desperdicio { get; set; }

        public bool? SeCompra { get; set; }

        public bool? SeVende { get; set; }

        public bool? EsFormula { get; set; }

        public int? TiempoEntrega { get; set; }

        public string? TiempoEntregaUnidad { get; set; }

        public int? TiempoEntregaSeg { get; set; }

        public string? TiempoEntregaSegUnidad { get; set; }

        public string? LoteOrdenar { get; set; }

        public double? CantidadOrdenar { get; set; }

        public double? MultiplosOrdenar { get; set; }

        public double? InvSeguridad { get; set; }

        public string? ProdRuta { get; set; }

        public string? AlmacenRop { get; set; }

        public string? CategoriaProd { get; set; }

        public double? ProdCantidad { get; set; }

        public string? ProdUsuario { get; set; }

        public int? ProdPasoTotal { get; set; }

        public string? ProdMovGrupo { get; set; }

        public string? ProdEstacion { get; set; }

        public bool ProdOpciones { get; set; }

        public bool? ProdVerConcentracion { get; set; }

        public bool? ProdVerCostoAcumulado { get; set; }

        public bool? ProdVerMerma { get; set; }

        public bool? ProdVerDesperdicio { get; set; }

        public bool? ProdVerPorcentaje { get; set; }

        public DateTime? RevisionUltima { get; set; }

        public string? RevisionUsuario { get; set; }

        public int? RevisionFrecuencia { get; set; }

        public string? RevisionFrecuenciaUnidad { get; set; }

        public DateTime? RevisionSiguiente { get; set; }

        public string? ProdMov { get; set; }

        public string? TipoCompra { get; set; }

        public bool? TieneMovimientos { get; set; }

        public string? Registro1 { get; set; }

        public DateTime? Registro1Vencimiento { get; set; }

        public string? AlmacenEspecificoVenta { get; set; }

        public string? AlmacenEspecificoVentaMov { get; set; }

        public string? RutaDistribucion { get; set; }

        public double? CostoEstandar { get; set; }

        public double? CostoReposicion { get; set; }

        public string? EstatusCosto { get; set; }

        public decimal? Margen { get; set; }

        public string? Proveedor { get; set; }

        public string? NivelAcceso { get; set; }

        public string? Temporada { get; set; }

        public bool? SolicitarPrecios { get; set; }

        public string? AutoRecaudacion { get; set; }

        public string? Concepto { get; set; }

        public string? Cuenta { get; set; }

        public double? Retencion1 { get; set; }

        public double? Retencion2 { get; set; }

        public double? Retencion3 { get; set; }

        public bool? Espacios { get; set; }

        public bool? EspaciosEspecificos { get; set; }

        public double? EspaciosSobreventa { get; set; }

        public string? EspaciosNivel { get; set; }

        public int? EspaciosMinutos { get; set; }

        public bool? EspaciosBloquearAnteriores { get; set; }

        public string? EspaciosHoraD { get; set; }

        public string? EspaciosHoraA { get; set; }

        public bool? SerieLoteInfo { get; set; }

        public double? CantidadMinimaVenta { get; set; }

        public double? CantidadMaximaVenta { get; set; }

        public double? EstimuloFiscal { get; set; }

        public string? OrigenPais { get; set; }

        public string? OrigenLocalidad { get; set; }

        public decimal? Incentivo { get; set; }

        public double? FactorCompra { get; set; }

        public double? Horas { get; set; }

        public bool? Isan { get; set; }

        public bool? ExcluirDescFormaPago { get; set; }

        public bool? EsDeducible { get; set; }

        public decimal? Peaje { get; set; }

        public string? CodigoAlterno { get; set; }

        public string? TipoCatalogo { get; set; }

        public bool? AnexosAlFacturar { get; set; }

        public int? CaducidadMinima { get; set; }

        public bool? Actividades { get; set; }

        public string? ValidarPresupuestoCompra { get; set; }

        public string? SeriesLotesAutoOrden { get; set; }

        public bool? LotesFijos { get; set; }

        public bool? LotesAuto { get; set; }

        public int? Consecutivo { get; set; }

        public string? TipoEmpaque { get; set; }

        public string? Modelo { get; set; }

        public string? Version { get; set; }

        public bool? TieneDireccion { get; set; }

        public string? Direccion { get; set; }

        public string? DireccionNumero { get; set; }

        public string? DireccionNumeroInt { get; set; }

        public string? EntreCalles { get; set; }

        public string? Plano { get; set; }

        public string? Observaciones { get; set; }

        public string? Colonia { get; set; }

        public string? Delegacion { get; set; }

        public string? Poblacion { get; set; }

        public string? Estado { get; set; }

        public string? Pais { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Ruta { get; set; }

        public string? Codigo { get; set; }

        public string? ClaveVehicular { get; set; }

        public string? TipoVehiculo { get; set; }

        public int? DiasLibresIntereses { get; set; }

        public bool? PrecioLiberado { get; set; }

        public bool? ValidarCodigo { get; set; }

        public string? Presentacion { get; set; }

        public int? GarantiaPlazo { get; set; }

        public bool? CostoIdentificado { get; set; }

        public double? CantidadTarima { get; set; }

        public string? UnidadTarima { get; set; }

        public double? MinimoTarima { get; set; }

        public int? DepartamentoDetallista { get; set; }

        public string? TratadoComercial { get; set; }

        public string? CuentaPresupuesto { get; set; }

        public string? ProgramaSectorial { get; set; }

        public string? PedimentoClave { get; set; }

        public string? PedimentoRegimen { get; set; }

        public string? Permiso { get; set; }

        public string? PermisoRenglon { get; set; }

        public string? Cuenta2 { get; set; }

        public string? Cuenta3 { get; set; }

        public bool? Impuesto1Excento { get; set; }

        public bool? CalcularPresupuesto { get; set; }

        public double? InflacionPresupuesto { get; set; }

        public bool? Excento2 { get; set; }

        public bool? Excento3 { get; set; }

        public string? ContUso { get; set; }

        public string? ContUso2 { get; set; }

        public string? ContUso3 { get; set; }

        public string? NivelToleranciaCosto { get; set; }

        public decimal? ToleranciaCosto { get; set; }

        public decimal? ToleranciaCostoInferior { get; set; }

        public string? ObjetoGasto { get; set; }

        public string? ObjetoGastoRef { get; set; }

        public string? ClavePresupuestalImpuesto1 { get; set; }

        public string? Isbn { get; set; }

        public string? Estructura { get; set; }

        public string? TipoImpuesto1 { get; set; }

        public string? TipoImpuesto2 { get; set; }

        public string? TipoImpuesto3 { get; set; }

        public string? TipoImpuesto4 { get; set; }

        public string? TipoImpuesto5 { get; set; }

        public string? TipoRetencion1 { get; set; }

        public string? TipoRetencion2 { get; set; }

        public string? TipoRetencion3 { get; set; }

        public byte[]? SincroId { get; set; }

        public int? SincroC { get; set; }

        public short Calificacion { get; set; }

        public string? ClavePresupuestalRetencion1 { get; set; }

        public bool? Saux { get; set; }

        public string? InforclavePrincipal { get; set; }

        public double? InforstockMinimo { get; set; }

        public double? InforstockMaximo { get; set; }

        public string? Inforprefijo { get; set; }

        public string? Inforsufijo { get; set; }

        public string? CodigoEstructura { get; set; }

        public string? TipoVariante { get; set; }

        public string? Infortipo { get; set; }

        public int? Inforcuarentena { get; set; }

        public string? InforclavePlanta { get; set; }

        public bool? Infortrazabilidad { get; set; }

        public bool? InforlotificacionPropia { get; set; }

        public int? InforultimoNumeroLote { get; set; }

        public int? InforunidadesMaximaLote { get; set; }

        public bool? InfortieneNoSerie { get; set; }

        public double? Inforsmr { get; set; }

        public string? InfortipoDeAsignacion { get; set; }

        public string? InfornoSerie { get; set; }

        public string? Inforformato { get; set; }

        public string? InforalmacenProd { get; set; }

        public string? ReferenciaIntelisisService { get; set; }

        public bool? EsFactory { get; set; }

        public double? AltoTarima { get; set; }

        public double? LargoTarima { get; set; }

        public double? AnchoTarima { get; set; }

        public double? TaraTarima { get; set; }

        public double? VolumenTarima { get; set; }

        public double? PesoTarima { get; set; }

        public double? CantidadCamaTarima { get; set; }

        public double? CamasTarima { get; set; }

        public int? EstibaMaxima { get; set; }

        public string? ControlArticulo { get; set; }

        public int? DiasControlCaducidad { get; set; }

        public bool? EstibaMismaFecha { get; set; }

        public string? TipoRotacion { get; set; }

        public bool? PermiteEstibar { get; set; }

        public bool? EmidaRecargaTelefonica { get; set; }

        public bool? EmidaTiempoAire { get; set; }

        public bool? Ldi { get; set; }

        public string? Ldiservicio { get; set; }

        public string? Posforma { get; set; }

        public string? ArticuloWeb { get; set; }

        public int? TarimasReacomodar { get; set; }

        public double? CantidadPresentacion { get; set; }

        public string MfatipoDeducibilidad { get; set; } = null!;

        public bool? EsCombustible { get; set; }

        public double? ProdRitmo { get; set; }

        public bool? MaterialPeligroso { get; set; }

        public string? PosagenteDetalle { get; set; }

        public int? TipoArticulo { get; set; }

        public string? AlmMesComs { get; set; }

        public double? MinimoCompra { get; set; }

        public double? StockMinimo { get; set; }

        public double? StockMaximo { get; set; }

        public int? Smr { get; set; }

        public Guid? CrmobjectId { get; set; }

        public string? Tono { get; set; }

        public string? ProdProceso { get; set; }

        public string? ProdConsecutivo { get; set; }

        public bool? Recuperacion { get; set; }

        public bool? RutaArticulo { get; set; }

        public string? ProdTipoArt { get; set; }

        public bool? ProdRutaSecuencial { get; set; }

        public double? ProdTiempoProceso { get; set; }

        public double? ProdCapacidadInstalada { get; set; }

        public double? ProdCapacidadReal { get; set; }

        public string? AlmacenDes { get; set; }

        public double? ProdDestajoBulto { get; set; }

        public bool? CesumarizaEnFactura { get; set; }

        public bool? CenoAplicaBeca { get; set; }

        public bool? CenoAplicaPorcMat { get; set; }

        public bool? Iedu { get; set; }

        public bool? MesEnIedu { get; set; }

        public bool? ProdEstructuraFam { get; set; }

        public string? CfdiretClave { get; set; }

        public string? CfdiretIepsimpuesto { get; set; }

        public string? DescripcionHtml { get; set; }

        public double? VicMedida { get; set; }

        public string? VicUso { get; set; }

        public string? VicNegociacion { get; set; }

        public string? VicInmueble { get; set; }

        public string? VicArea { get; set; }

        public string? VicSubArea { get; set; }

        public double? VicIndiviso { get; set; }

        public double? VicFactor1 { get; set; }

        public double? VicFactor2 { get; set; }

        public double? VicFactor3 { get; set; }

        public int? VicMesesRelComer { get; set; }

        public DateTime? VicFechaEstimOper { get; set; }

        public bool? VicPromPlanos { get; set; }

        public bool? VicPropio { get; set; }

        public bool? VicComplemento { get; set; }

        public string? VicContratoId { get; set; }

        public string? VicPredial { get; set; }

        public string? VicNivel { get; set; }

        public string? VicSubNivel { get; set; }

        public string? VicContratoIdcargoExcepcion { get; set; }

        public string? VicEstatus { get; set; }

        public double? VicMedidaEstimada { get; set; }

        public bool? TieneContrato { get; set; }

        public string? Comentarios { get; set; }

        public string? Proyecto { get; set; }

        public string? Html { get; set; }

        public int? VicContratoId2 { get; set; }

        public string? VicEstatus2 { get; set; }

        public decimal? VicImporte1 { get; set; }

        public decimal? VicImporte2 { get; set; }

        public decimal? VicImporte3 { get; set; }

        public string? VicOrigen { get; set; }

        public decimal? PrecioVentaM2 { get; set; }

        public DateTime? VicFechaAlta { get; set; }

        public bool? VicVenta { get; set; }

        public bool? VicRenta { get; set; }

        public bool? VicRentaSventa { get; set; }

        public bool? VicCompra { get; set; }

        public bool? VicSubArrendamiento { get; set; }

        public bool? VicIntermediario { get; set; }

        public bool? VicArrendamiento { get; set; }

        public decimal? VicCostoObra { get; set; }

        public decimal? VicCostoTerreno { get; set; }

        public int? IdCuadroBasico { get; set; }

        public bool? ExcluirPlaneacion { get; set; }

        public double? PesoInterno { get; set; }

        public double? MapaLatitud { get; set; }

        public double? MapaLongitud { get; set; }

        public string? MapaUbicacion { get; set; }

        public bool Fae { get; set; }

        public decimal? Faeporcentaje { get; set; }

        public bool? Sugeridos { get; set; }

        public bool? ImpuestosLocalesCfdi { get; set; }

        public bool EsMes { get; set; }

        public string? UnidadMes { get; set; }

        public string? UnidadConvermes { get; set; }

        public double AplicaRedondeo { get; set; }

        public double MultiploFabricacion { get; set; }

        public double MultiploConsumo { get; set; }
    }
}