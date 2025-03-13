using INTELISIS.APPCORE.EL.Common;

namespace INTELISIS.APPCORE.EL
{
    /// <summary>
    /// Clase para asignar los parametros de configuración del API
    /// </summary>
    public class AppSettings
    {
        #region Campos

        private static string datosconexion;
        private static string datosconexion2;
        private static string directorioimagenesarticulos;
        private static string hostparacors;
        private static string hostparacors2;
        private static string directorioroot;
        private static string secret;
        private static string usuarioimagenes;
        private static string contrasenaimagenes;
        private static string urlapi;
        private static string rutaanexos;
        private static string usuarioanexos;
        private static string passanexos;
        private static KeyValueModel pathEvidencias;
        private static string pathLogosSubdistribuidor;
        private static string rutaLogistica;
        private static string usuarioLogistica;
        private static string passLogistica;
        private static int tokenExpiresInDays;
        private static int tokenExpiresInDaysAppSD;
        private static string destinatariosChatBot;
        private static string sessionKey;
        private static string physicalPathInfraestructura;
        private static string dbName;
        private static KeyValueModel rutaAnexosControlCalidad;
        private static AppSettingsToken tokenSettings;
        private static string basicEncryptKey;
        #endregion Campos

        #region Propiedades
        public static string PhysicalPathInfraestructura { get => physicalPathInfraestructura; set => physicalPathInfraestructura = value; }
        public static string SessionKey { get => sessionKey; set => sessionKey = value; }
        public static string DatosConexion { get => datosconexion; set => datosconexion = value; }
        public static string DatosConexion2 { get => datosconexion2; set => datosconexion2 = value; }
        public static string DirectorioImagenesArticulos { get => directorioimagenesarticulos; set => directorioimagenesarticulos = value; }
        public static string HostParaCors { get => hostparacors; set => hostparacors = value; }
        public static string HostParaCors2 { get => hostparacors2; set => hostparacors2 = value; }
        public static string DirectorioRoot { get => directorioroot; set => directorioroot = value; }
        public static string Secret { get => secret; set => secret = value; }
        public static string UsuarioImagenes { get => usuarioimagenes; set => usuarioimagenes = value; }
        public static string ContrasenaImagenes { get => contrasenaimagenes; set => contrasenaimagenes = value; }
        public static string UrlAPI { get => urlapi; set => urlapi = value; }
        public static string RutaAnexos { get => rutaanexos; set => rutaanexos = value; }
        public static string UsuarioAnexos { get => usuarioanexos; set => usuarioanexos = value; }
        public static string PassAnexos { get => passanexos; set => passanexos = value; }
        public static string BasicEncryptKey { get => basicEncryptKey; set => basicEncryptKey = value; }

        /// <summary>
        /// Ruta donde se guardan las evidencias de entregas a clientes 
        /// Value: La ruta fisica donde se guardan las evidencias
        /// Key: La ruta de acceso publica a las evidencias (middleware)
        /// </summary>
        public static KeyValueModel PathEvicendias { get => pathEvidencias; set => pathEvidencias = value; }
        public static string PathLogosSubdistribuidor { get => pathLogosSubdistribuidor; set => pathLogosSubdistribuidor = value; }
        public static string RutaLogistica { get => rutaLogistica; set => rutaLogistica = value; }
        public static string UsuarioLogistica { get => usuarioLogistica; set => usuarioLogistica = value; }
        public static string PassLogistica { get => passLogistica; set => passLogistica = value; }
        public static int TokenExpiresInDays { get => tokenExpiresInDays; set => tokenExpiresInDays = value; }
        public static int TokenExpiresInDaysAppSD { get => tokenExpiresInDaysAppSD; set => tokenExpiresInDaysAppSD = value; }
        public static string DestinatariosChatBot { get => destinatariosChatBot; set => destinatariosChatBot = value; }
        public static string DbName { get => dbName; set => dbName = value; }
        public static KeyValueModel RutaAnexosControlCalidad { get => rutaAnexosControlCalidad; set => rutaAnexosControlCalidad = value; }
        public static AppSettingsToken TokenSettings { get => tokenSettings; set => tokenSettings = value; }

        #endregion Propiedades
    }


    public class AppSettingsToken
    {
        private string secret;
        private string issuer;
        private string audience;
        private int expiration;

        public string Secret { get => secret; set => secret = value; }
        public string Issuer { get => issuer; set => issuer = value; }
        public string Audience { get => audience; set => audience = value; }
        public int Expiration { get => expiration; set => expiration = value; }
    }
}