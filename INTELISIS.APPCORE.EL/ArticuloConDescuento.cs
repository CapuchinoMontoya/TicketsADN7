namespace INTELISIS.APPCORE.EL
{
    public class ArticuloConDescuento
    {
        public string Articulo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto1 { get; set; }
        public decimal Impuesto2 { get; set; }
        public decimal PrecioTotal { get; set; }
        public double PorcentajeDescuento { get; set; }

    }
}