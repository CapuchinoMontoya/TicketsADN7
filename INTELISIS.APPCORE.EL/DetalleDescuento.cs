namespace INTELISIS.APPCORE.EL
{
    public class DetalleDescuento
    {
        public string? Articulo { get; set; }
        public int? Cantidad { get; set; }
        public double? DescuentoAplicado { get; set; }
        public decimal? PrecioBase { get; set; }
        public decimal? PrecioConDescuento { get; set; }
        public decimal? Impuesto1 { get; set; }
        public decimal? Impuesto2 { get; set; }
        public decimal? PrecioFinal { get; set; }
    }
}