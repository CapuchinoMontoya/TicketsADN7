using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class Alm
    {
        public string Almacen { get; set; } = null!;

        public string? Rama { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? EntreCalles { get; set; }

        public string? Plano { get; set; }

        public string? Observaciones { get; set; }

        public string? Colonia { get; set; }

        public string? Delegacion { get; set; }

        public string? Poblacion { get; set; }

        public string? Estado { get; set; }

        public string? Pais { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Encargado { get; set; }

        public string? Telefonos { get; set; }

        public string? Grupo { get; set; }

        public string? Categoria { get; set; }

        public string? Zona { get; set; }

        public string? Ruta { get; set; }

        public string? Tipo { get; set; }

        public int Sucursal { get; set; }

        public string? Exclusivo { get; set; }

        public int? Orden { get; set; }

        public string Estatus { get; set; } = null!;

        public DateTime? UltimoCambio { get; set; }

        public DateTime? Alta { get; set; }

        public bool Logico1 { get; set; }

        public bool Logico2 { get; set; }

        public bool FacturasPendientes { get; set; }

        public bool? WMostrar { get; set; }

        public bool? WUnicamenteDisponibles { get; set; }

        public bool? TieneMovimientos { get; set; }

        public string? NivelAcceso { get; set; }

        public string? Idioma { get; set; }

        public bool? ExcluirPlaneacion { get; set; }

        public string? Cbdir { get; set; }

        public string? Cuenta { get; set; }

        public bool? Segundas { get; set; }

        public bool? Compartido { get; set; }

        public int? SucursalRef { get; set; }

        public bool? Wms { get; set; }

        public string? PosicionDef { get; set; }

        public bool? GenerarOrdenEntarimado { get; set; }

        public bool? GenerarSolAcomodoRecibo { get; set; }

        public bool? GenerarOrdenAcomodoRecibo { get; set; }

        public bool? GenerarSolAcomodoSurtido { get; set; }

        public bool? GenerarOrdenAcomodoSurtido { get; set; }

        public bool? SugerirSurtidoTarima { get; set; }

        public string? DireccionNumero { get; set; }

        public string? DireccionNumeroInt { get; set; }

        public double? MapaLatitud { get; set; }

        public double? MapaLongitud { get; set; }

        public int? MapaPrecision { get; set; }

        public bool? PermiteRechazos { get; set; }

        public bool? PermiteUbicaciones { get; set; }

        public bool? EsAlmacenDeDeposito { get; set; }

        public bool? EsAlmacenMaterialesExteriores { get; set; }

        public bool? NoDisponibleConsumos { get; set; }

        public string? ContactoTipo { get; set; }

        public string? DefPosicionRecibo { get; set; }

        public string? DefPosicionSurtido { get; set; }

        public bool? ECommerceSincroniza { get; set; }

        public string? InforclavePlanta { get; set; }

        public string? ReferenciaIntelisisService { get; set; }

        public bool? EsFactory { get; set; }

        public bool Ubicaciones { get; set; }

        public string? MapaUbicacion { get; set; }

        public string? Defposicioncrossdocking { get; set; }

        public string? Escrossdocking { get; set; }

        public bool Mes { get; set; }

        public Guid? CrmobjectId { get; set; }

        public DateTime? CrmlastUpdate { get; set; }

        public bool? Cedis { get; set; }

        public bool? CompraDirecta { get; set; }
    }
}