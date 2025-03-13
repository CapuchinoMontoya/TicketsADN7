using System;
using System.Collections.Generic;

namespace INTELISIS.APPCORE.EL
{
    public partial class ListaPreciosD
    {
        public string Lista { get; set; } = null!;

        public string Moneda { get; set; } = null!;

        public string Articulo { get; set; } = null!;

        public decimal? Precio { get; set; }

        public string? CodigoCliente { get; set; }

        public double? Margen { get; set; }

        public string? Region { get; set; }

        public Guid? CrmobjectId { get; set; }

        public DateTime? CrmlastUpdate { get; set; }

        public string? Inmueble { get; set; }

        public string? InmuebleNombre { get; set; }

        public string? Operacion { get; set; }

        public string? Area { get; set; }

        public string? Aplica { get; set; }

        public double? MetrosCuadrados { get; set; }

        public decimal? PrecioMinMetro { get; set; }

        public decimal? PrecioMinimo { get; set; }

        public decimal? PrecioMaxMetro { get; set; }

        public decimal? PrecioMaximo { get; set; }

        public double? PjeComisionVenta { get; set; }

        public double? PjeEngancheMinimo { get; set; }

        public double? PlazoMinimo { get; set; }

        public decimal? RentaMinMetro { get; set; }

        public decimal? RentaMinima { get; set; }

        public decimal? RentaMaxMetro { get; set; }

        public decimal? RentaMaxima { get; set; }

        public double? ComisionMeses { get; set; }

        public int? PlazoMaximo { get; set; }

        public double? PjeMantto { get; set; }

        public decimal? ManttoMin { get; set; }

        public decimal? ManttoMax { get; set; }

        public double? PjePublicidad { get; set; }

        public decimal? PublicidadMin { get; set; }

        public decimal? PublicidadMax { get; set; }

        public int? DepositoMinMeses { get; set; }

        public int? DepositoMaxMeses { get; set; }

        public decimal? DerechoOfertaMin { get; set; }

        public decimal? DerechoOfertaMax { get; set; }

        public int? RevContratoMeses { get; set; }

        public decimal? RentaBaseMinVenta { get; set; }

        public decimal? RentaBaseMaxVenta { get; set; }

        public double? PjeRentaVenta { get; set; }

        public int? MesesMinRentaAdelante { get; set; }

        public int? MesesMaxRentaAdelante { get; set; }

        public int? MesesMinFondoGarantia { get; set; }

        public int? MesesMaxFondoGarantia { get; set; }

        public decimal? PrecioA { get; set; }
    }
}