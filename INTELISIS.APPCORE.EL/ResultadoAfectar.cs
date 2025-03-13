namespace INTELISIS.APPCORE.EL
{
    /// <summary>
    /// Clase para la información del registro resultado al Afectar algun movimiento
    /// </summary>
    public class ResultadoAfectar
    {
        #region Campos

        int ok;
        string titulo;
        string nivel;
        string descripcion;
        int id;
        object resultado;

        #endregion

        #region Propiedades

        public int Ok { get => ok; set => ok = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Nivel { get => nivel; set => nivel = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int ID { get => id; set => id = value; }
        public object Resultado { get => resultado; set => resultado = value; }

        #endregion
    }
}
