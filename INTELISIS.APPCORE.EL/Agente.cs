using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Agente
    {
        public string Agente1 { get; set; } = null!;

        public string? Nombre { get; set; }

        public string? Tipo { get; set; }

        public string? Moneda { get; set; }

        public string? Telefonos { get; set; }

        public string? Extencion { get; set; }

        public string? Categoria { get; set; }

        public string? Familia { get; set; }

        public string? Zona { get; set; }

        public string? Grupo { get; set; }

        public string Estatus { get; set; } = null!;

        public DateTime? UltimoCambio { get; set; }

        public string? Clase { get; set; }

        public DateTime? Alta { get; set; }

        public DateTime? Baja { get; set; }

        public bool Conciliar { get; set; }

        public string? Mensaje { get; set; }

        public string? BeneficiarioNombre { get; set; }

        public double? CostoHora { get; set; }

        public string? TipoComision { get; set; }

        public double? Porcentaje { get; set; }

        public bool Nomina { get; set; }

        public string? Personal { get; set; }

        public string? NominaMov { get; set; }

        public string? NominaConcepto { get; set; }

        public string? Direccion { get; set; }

        public string? Colonia { get; set; }

        public string? Poblacion { get; set; }

        public string? Estado { get; set; }

        public string? Pais { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Rfc { get; set; }

        public string? Curp { get; set; }

        public bool? TieneMovimientos { get; set; }

        public string? NivelAcceso { get; set; }

        public int? SucursalEmpresa { get; set; }

        public bool Logico1 { get; set; }

        public bool Logico2 { get; set; }

        public bool Equipo { get; set; }

        public decimal? Cuota { get; set; }

        public string? ArticuloDef { get; set; }

        public string? AlmacenDef { get; set; }

        public string? Acreedor { get; set; }

        public string? EMail { get; set; }

        public bool? EMailAuto { get; set; }

        public bool? VentasCasa { get; set; }

        public string? ReportaA { get; set; }

        public string? Jornada { get; set; }

        public string? DireccionNumero { get; set; }

        public string? DireccionNumeroInt { get; set; }

        public double? MapaLatitud { get; set; }

        public double? MapaLongitud { get; set; }

        public int? MapaPrecision { get; set; }

        public bool? FueraLinea { get; set; }

        public string? ContrasenaWeb { get; set; }

        public decimal? Comision1 { get; set; }

        public string? ContactoTipo { get; set; }

        public Guid? Crmid { get; set; }

        public string? DomainName { get; set; }

        public string? ClerkId { get; set; }

        public string? Tmaprefijo { get; set; }

        public string? MapaUbicacion { get; set; }

        public Guid? CrmobjectId { get; set; }

        public string? FiscalRegimen { get; set; }
    }
}