using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Cte
    {
        public string Cliente { get; set; } = null!;

        public string? Rama { get; set; }

        public string? Nombre { get; set; }

        public string? NombreCorto { get; set; }

        public string? Direccion { get; set; }

        public string? DireccionNumero { get; set; }

        public string? DireccionNumeroInt { get; set; }

        public string? EntreCalles { get; set; }

        public string? Plano { get; set; }

        public string? Observaciones { get; set; }

        public string? Delegacion { get; set; }

        public string? Colonia { get; set; }

        public string? Poblacion { get; set; }

        public string? Estado { get; set; }

        public string? Pais { get; set; }

        public string? Zona { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Rfc { get; set; }

        public string? Ieps { get; set; }

        public string? Pitex { get; set; }

        public string? Curp { get; set; }

        public string? Telefonos { get; set; }

        public string? TelefonosLada { get; set; }

        public string? Fax { get; set; }

        public bool PedirTono { get; set; }

        public string? Contacto1 { get; set; }

        public string? Contacto2 { get; set; }

        public string? Extencion1 { get; set; }

        public string? Extencion2 { get; set; }

        public string? EMail1 { get; set; }

        public string? EMail2 { get; set; }

        public string? DirInternet { get; set; }

        public string? Categoria { get; set; }

        public string? Grupo { get; set; }

        public string? Familia { get; set; }

        public string? Credito { get; set; }

        public string? DiaRevision1 { get; set; }

        public string? DiaRevision2 { get; set; }

        public string? HorarioRevision { get; set; }

        public string? DiaPago1 { get; set; }

        public string? DiaPago2 { get; set; }

        public string? HorarioPago { get; set; }

        public string? ZonaImpuesto { get; set; }

        public bool PedidosParciales { get; set; }

        public string? Tipo { get; set; }

        public string? Situacion { get; set; }

        public DateTime? SituacionFecha { get; set; }

        public string? SituacionUsuario { get; set; }

        public string? SituacionNota { get; set; }

        public string? Descuento { get; set; }

        public string? Agente { get; set; }

        public string? AgenteServicio { get; set; }

        public string? Agente3 { get; set; }

        public string? Agente4 { get; set; }

        public int? EnviarA { get; set; }

        public string? Proyecto { get; set; }

        public string? FormaEnvio { get; set; }

        public string? Condicion { get; set; }

        public string? Ruta { get; set; }

        public int? RutaOrden { get; set; }

        public int? ListaPrecios { get; set; }

        public string? ListaPreciosEsp { get; set; }

        public string? DefMoneda { get; set; }

        public bool VtasConsignacion { get; set; }

        public string? AlmacenVtasConsignacion { get; set; }

        public string? Clase { get; set; }

        public string Estatus { get; set; } = null!;

        public DateTime? UltimoCambio { get; set; }

        public DateTime? Alta { get; set; }

        public bool Conciliar { get; set; }

        public string? Mensaje { get; set; }

        public int? Numero { get; set; }

        public string? Contrasena { get; set; }

        public string? Contrasena2 { get; set; }

        public bool? WVerDisponible { get; set; }

        public bool? WVerArtListaPreciosEsp { get; set; }

        public string? ChecarCredito { get; set; }

        public string? BloquearMorosos { get; set; }

        public string? ModificarVencimiento { get; set; }

        public bool CreditoEspecial { get; set; }

        public bool CreditoConLimite { get; set; }

        public decimal? CreditoLimite { get; set; }

        public bool? CreditoConLimitePedidos { get; set; }

        public decimal? CreditoLimitePedidos { get; set; }

        public string? CreditoMoneda { get; set; }

        public bool CreditoConDias { get; set; }

        public int? CreditoDias { get; set; }

        public bool CreditoConCondiciones { get; set; }

        public string? CreditoCondiciones { get; set; }

        public bool? TieneMovimientos { get; set; }

        public string? Cobrador { get; set; }

        public string? PersonalCobrador { get; set; }

        public bool? DescuentoRecargos { get; set; }

        public string? RecorrerVencimiento { get; set; }

        public string? AlmacenDef { get; set; }

        public int? SucursalEmpresa { get; set; }

        public string? NivelAcceso { get; set; }

        public string? Idioma { get; set; }

        public string? BonificacionTipo { get; set; }

        public double? Bonificacion { get; set; }

        public DateTime? VigenciaDesde { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public string? Descripcion1 { get; set; }

        public string? Descripcion2 { get; set; }

        public string? Descripcion3 { get; set; }

        public string? Descripcion4 { get; set; }

        public string? Descripcion5 { get; set; }

        public string? Descripcion6 { get; set; }

        public string? Descripcion7 { get; set; }

        public string? Descripcion8 { get; set; }

        public string? Descripcion9 { get; set; }

        public string? Descripcion10 { get; set; }

        public string? Descripcion11 { get; set; }

        public string? Descripcion12 { get; set; }

        public string? Descripcion13 { get; set; }

        public string? Descripcion14 { get; set; }

        public string? Descripcion15 { get; set; }

        public string? Descripcion16 { get; set; }

        public string? Descripcion17 { get; set; }

        public string? Descripcion18 { get; set; }

        public string? Descripcion19 { get; set; }

        public string? Descripcion20 { get; set; }

        public bool? FormasPagoRestringidas { get; set; }

        public bool? PreciosInferioresMinimo { get; set; }

        public string? Cbdir { get; set; }

        public string? PersonalNombres { get; set; }

        public string? PersonalApellidoPaterno { get; set; }

        public string? PersonalApellidoMaterno { get; set; }

        public string? PersonalDireccion { get; set; }

        public string? PersonalEntreCalles { get; set; }

        public string? PersonalPlano { get; set; }

        public string? PersonalDelegacion { get; set; }

        public string? PersonalColonia { get; set; }

        public string? PersonalPoblacion { get; set; }

        public string? PersonalEstado { get; set; }

        public string? PersonalPais { get; set; }

        public string? PersonalZona { get; set; }

        public string? PersonalCodigoPostal { get; set; }

        public string? PersonalTelefonos { get; set; }

        public string? PersonalTelefonosLada { get; set; }

        public string? PersonalTelefonoMovil { get; set; }

        public bool? PersonalSms { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string? Titulo { get; set; }

        public string? EstadoCivil { get; set; }

        public DateTime? FechaMatrimonio { get; set; }

        public string? Conyuge { get; set; }

        public string? Sexo { get; set; }

        public bool? Fuma { get; set; }

        public string? Profesion { get; set; }

        public string? Puesto { get; set; }

        public int? NumeroHijos { get; set; }

        public string? Alergias { get; set; }

        public string? Religion { get; set; }

        public string? Responsable { get; set; }

        public string? Parentesco { get; set; }

        public string? FacturarCte { get; set; }

        public int? FacturarCteEnviarA { get; set; }

        public string? Aseguradora { get; set; }

        public string? NombreAsegurado { get; set; }

        public string? PolizaTipo { get; set; }

        public string? PolizaNumero { get; set; }

        public string? PolizaReferencia { get; set; }

        public decimal? PolizaImporte { get; set; }

        public decimal? Deducible { get; set; }

        public string? DeducibleMoneda { get; set; }

        public double? Coaseguro { get; set; }

        public string? Espacio { get; set; }

        public string? OtrosCargos { get; set; }

        public bool? Flotilla { get; set; }

        public string? FordDistribuidor { get; set; }

        public string? FordZona { get; set; }

        public bool? ExcentoIsan { get; set; }

        public decimal? Crmimporte { get; set; }

        public double? Crmcantidad { get; set; }

        public string? Crmetapa { get; set; }

        public double? CrmcierreProbabilidad { get; set; }

        public DateTime? CrmcierreFechaAprox { get; set; }

        public decimal? CrmpresupuestoAsignado { get; set; }

        public string? Crmcompetencia { get; set; }

        public string? Crminfluencia { get; set; }

        public string? Crmfuente { get; set; }

        public DateTime? Fecha1 { get; set; }

        public DateTime? Fecha2 { get; set; }

        public DateTime? Fecha3 { get; set; }

        public DateTime? Fecha4 { get; set; }

        public DateTime? Fecha5 { get; set; }

        public bool? EsProveedor { get; set; }

        public bool? EsPersonal { get; set; }

        public bool? EsAgente { get; set; }

        public bool? EsAlmacen { get; set; }

        public bool? EsEspacio { get; set; }

        public bool? EsCentroCostos { get; set; }

        public bool? EsProyecto { get; set; }

        public bool? EsCentroTrabajo { get; set; }

        public bool? EsEstacionTrabajo { get; set; }

        public string? Usuario { get; set; }

        public int? Licencias { get; set; }

        public string? LicenciasTipo { get; set; }

        public string? LicenciasLlave { get; set; }

        public string? Cuenta { get; set; }

        public string? CuentaRetencion { get; set; }

        public string? FiscalRegimen { get; set; }

        public bool? PedidoDef { get; set; }

        public bool? EMailAuto { get; set; }

        public string? WMovVentas { get; set; }

        public bool? Intercompania { get; set; }

        public bool? Publico { get; set; }

        public string? CrmovVenta { get; set; }

        public bool? Extranjero { get; set; }

        public bool? DocumentacionCompleta { get; set; }

        public decimal? OperacionLimite { get; set; }

        public string? ImportadorRegimen { get; set; }

        public string? ImportadorRegistro { get; set; }

        public string? LocalidadCnbv { get; set; }

        public string? ActividadEconomicaCnbv { get; set; }

        public string? Grado { get; set; }

        public string? Gln { get; set; }

        public string? InterfacturaRi { get; set; }

        public string? Ediidentificador { get; set; }

        public string? Edicalificador { get; set; }

        public string? ProveedorClave { get; set; }

        public string? ProveedorInfo { get; set; }

        public string? Rpu { get; set; }

        public string? Sirac { get; set; }

        public string? Ife { get; set; }

        public string? Pasaporte { get; set; }

        public string? GrupoSanguineo { get; set; }

        public string? GrupoSanguineoRh { get; set; }

        public double? Peso { get; set; }

        public double? Estatura { get; set; }

        public string? Comentarios { get; set; }

        public DateTime? PolizaFechaEmision { get; set; }

        public DateTime? PolizaVencimiento { get; set; }

        public string? NotificarA { get; set; }

        public string? NoriticarAtelefonos { get; set; }

        public string? ExpedienteFamiliar { get; set; }

        public string? Sic { get; set; }

        public string? ReferenciaBancaria { get; set; }

        public double? MapaLatitud { get; set; }

        public double? MapaLongitud { get; set; }

        public int? MapaPrecision { get; set; }

        public bool? FueraLinea { get; set; }

        public string? TipoRegistro { get; set; }

        public bool? FiscalGenerar { get; set; }

        public byte[]? SincroId { get; set; }

        public int? SincroC { get; set; }

        public string? ContrasenaWeb { get; set; }

        public string? Crmid { get; set; }

        public bool? SincronizarCrm { get; set; }

        public string? PrimaryContactId { get; set; }

        public string? TemaCrm { get; set; }

        public string? FiscalZona { get; set; }

        public string? ContactoTipo { get; set; }

        public string? ReferenciaIntelisisService { get; set; }

        public string? DefPosicionRecibo { get; set; }

        public string? DefPosicionSurtido { get; set; }

        public bool? TarimasChep { get; set; }

        public string MfatipoOperacion { get; set; } = null!;

        public string? CtaBanco { get; set; }

        public int? ClaveBanco { get; set; }

        public string? ClabeCuenta { get; set; }

        public string? Tarjeta { get; set; }

        public string? MapaUbicacion { get; set; }

        public Guid? CrmobjectId { get; set; }

        public string? MovimientoUltimoCobro { get; set; }

        public DateTime? FechaUltimoCobro { get; set; }

        public DateTime? FechaUltimoCobroAux { get; set; }

        public string? MovimientoUltimoCobroAux { get; set; }

        public bool CalculoMoratorioMavi { get; set; }

        public string? Latitud { get; set; }

        public string? Longitud { get; set; }

        public string? PaginaWeb { get; set; }

        public string? EMail { get; set; }

        public string? NombreComercial { get; set; }

        public string? ReferidoNombre { get; set; }

        public string? ReferidoMail { get; set; }

        public string? ReferidoTelefono { get; set; }

        public string? ReferidoRfc { get; set; }

        public string? RelacionReferencia { get; set; }

        public string? Origen { get; set; }

        public bool? EsFavorito { get; set; }

        public string? Ubicacion { get; set; }

        public bool? VicNoValidarContrato { get; set; }

        public string? DireccionMax { get; set; }

        public string? PersonalNacionalidadCve { get; set; }

        public string? PersonalNacionalidad { get; set; }

        public string? PersonalEstadoCve { get; set; }

        public string? PersonalMunicipioCve { get; set; }

        public string? PersonalLocalidadCve { get; set; }

        public string? ClaveEce { get; set; }

        public bool? PacienteExterno { get; set; }

        public string? PersonalCveTipoVialidad { get; set; }

        public string? PersonalTipoVialidad { get; set; }

        public string? PersonalCveTipoAsentamiento { get; set; }

        public string? PersonalTipoAsentamiento { get; set; }

        public string? PersonalVialidad { get; set; }

        public string? PersonalNumInt { get; set; }

        public string? PersonalNumExt { get; set; }

        public string? PersonalEstadoNacimientoCve { get; set; }

        public string? PersonalEstadoNacimiento { get; set; }

        public string? FolioActivo { get; set; }

        public DateTime? FolioActivoFecha { get; set; }

        public bool? Desconocido { get; set; }

        public bool? PersonalBuscaResp { get; set; }

        public bool? Embarazo { get; set; }

        public string? CuentaAnticipo { get; set; }

        public string? CuentaCompleAnticipo { get; set; }

        public string? MunicipioNacimiento { get; set; }

        public bool? NacioExt { get; set; }

        public bool? Migrante2 { get; set; }

        public string? Referencia { get; set; }

        public string? TipoPacientePago { get; set; }

        public bool? ResideExt { get; set; }

        public bool? AccesoWeb { get; set; }

        public string? RolWeb { get; set; }

        public string? ContrasenaWebConfir { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public string? DefEmpresaWeb { get; set; }

        public int? DefSucursalWeb { get; set; }

        public string? UsuarioSt { get; set; }

        public string? UsuarioResp { get; set; }
    }
}