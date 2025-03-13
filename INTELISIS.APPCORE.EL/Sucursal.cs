using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Sucursal
    {
        public int Sucursal1 { get; set; }

        public string? Nombre { get; set; }

        public string? Prefijo { get; set; }

        public string Relacion { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? DireccionNumero { get; set; }

        public string? DireccionNumeroInt { get; set; }

        public string? Delegacion { get; set; }

        public string? Gln { get; set; }

        public string? Colonia { get; set; }

        public string? Poblacion { get; set; }

        public string? Estado { get; set; }

        public string? Pais { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Telefonos { get; set; }

        public string? Fax { get; set; }

        public string Estatus { get; set; } = null!;

        public DateTime? UltimoCambio { get; set; }

        public string? Rfc { get; set; }

        public string? RegistroPatronal { get; set; }

        public string? Encargado { get; set; }

        public DateTime? Alta { get; set; }

        public string? Region { get; set; }

        public bool? CentralRegional { get; set; }

        public bool? EnLinea { get; set; }

        public int? SucursalPrincipal { get; set; }

        public string? ListaPreciosEsp { get; set; }

        public bool? Cajeros { get; set; }

        public string? CentroCostos { get; set; }

        public bool? OperacionContinua { get; set; }

        public string? ZonaEconomica { get; set; }

        public string? Grupo { get; set; }

        public string? AlmacenPrincipal { get; set; }

        public string? Servidor { get; set; }

        public string? BaseDatos { get; set; }

        public string? Usuario { get; set; }

        public string? ZonaImpuesto { get; set; }

        public string? CajaCentral { get; set; }

        public string? WMovVentas { get; set; }

        public string? WAlmacen { get; set; }

        public string? WAgente { get; set; }

        public string? WUsuario { get; set; }

        public int? WUen { get; set; }

        public string? WConcepto { get; set; }

        public string? CrtipoCredito { get; set; }

        public string? Cliente { get; set; }

        public string? Categoria { get; set; }

        public string? Acreedor { get; set; }

        public string? LocalidadCnbv { get; set; }

        public string? Tipo { get; set; }

        public DateTime? FechaApertura { get; set; }

        public DateTime? VencimientoContrato { get; set; }

        public double? Metros { get; set; }

        public string? CostoBase { get; set; }

        public DateTime? UltimaSincronizacion { get; set; }

        public string? Ip { get; set; }

        public bool? Ipdinamica { get; set; }

        public int? Ippuerto { get; set; }

        public bool? ComunicacionEncriptada { get; set; }

        public double? MapaLatitud { get; set; }

        public double? MapaLongitud { get; set; }

        public int? MapaPrecision { get; set; }

        public bool? BloquearCobroTarjeta { get; set; }

        public string? FiscalRegimen { get; set; }

        public bool? Cfdflex { get; set; }

        public string? NoCertificado { get; set; }

        public string? Llave { get; set; }

        public string? ContrasenaSello { get; set; }

        public string? CertificadoBase64 { get; set; }

        public string? RutaCertificado { get; set; }

        public string? FiscalZona { get; set; }

        public string? ReferenciaIntelisisService { get; set; }

        public string? SucursalLdi { get; set; }

        public bool? ECommerce { get; set; }

        public bool? ECommerceOffLine { get; set; }

        public string? ECommerceSucursal { get; set; }

        public string? ECommerceImagenes { get; set; }

        public string? ECommerceAlmacen { get; set; }

        public string? ECommerceListaPrecios { get; set; }

        public string? ECommercePedido { get; set; }

        public string? ECommerceEstrategiaDescuento { get; set; }

        public string? ECommerceArticuloFlete { get; set; }

        public string? ECommerceTipoConsecutivo { get; set; }

        public string? ECommerceTipoConsecutivoFact { get; set; }

        public string? ECommerceCondicion { get; set; }

        public string? ECommerceCajero { get; set; }

        public string? ECommerceCteCat { get; set; }

        public string? ECommerceAgente { get; set; }

        public bool? ECommerceSincroniza { get; set; }

        public bool? ECommerceImpuestoIncluido { get; set; }

        public bool? PosmensajeLimiteCaja { get; set; }

        public string? Host { get; set; }

        public double? PoslimiteCaja { get; set; }

        public string? ParamAccDirIntelisisMes { get; set; }

        public bool? ECommerceConsultaExistencias { get; set; }

        public string? ECommerceUrl { get; set; }

        public int? ECommerceEnvolturaOmision { get; set; }

        public string? ECommerceCrmodo { get; set; }

        public string? ECommerceCrarticulo { get; set; }

        public string? ECommerceCrarticuloAplica { get; set; }

        public double? ECommerceCrminimo { get; set; }

        public double? ECommerceCrmaximo { get; set; }

        public string? ECommerceCrtipo { get; set; }

        public string? ECommerceCrconcepto { get; set; }

        public double? DifHorariaVerano { get; set; }

        public double? DifHorariaInvierno { get; set; }

        public string? MapaUbicacion { get; set; }

        public string? PinPadUsuario { get; set; }

        public string? PinPadContrasena { get; set; }

        public string? PinPadMoneda { get; set; }

        public bool? EnviaCorreo { get; set; }

        public string? Perfil { get; set; }

        public string? Remitente { get; set; }

        public string? Bdmes { get; set; }

        public string? Directora { get; set; }

        public string? Supervisor { get; set; }

        public string? JefeSector { get; set; }

        public string? Sector { get; set; }

        public string? Zona { get; set; }

        public bool? PosOmniAlmacen { get; set; }

        public string? PosPaisOrigenSuc { get; set; }

        public string? PosCteTipo { get; set; }

        public string? PosCteTipoOtro { get; set; }

        public double? PoslimiteCaja2 { get; set; }

        public string? DireccionServicio { get; set; }
    }
}