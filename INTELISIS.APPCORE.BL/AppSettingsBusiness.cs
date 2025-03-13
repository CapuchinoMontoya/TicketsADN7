using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.EL.Common;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra los parametros de configuración del API
    /// </summary>
    public class AppSettingsBusiness
    {
        #region Acción

        /// <summary>
        /// Método que asigna la cadena de conexión a la variable estatica de la clase AppSettings
        /// </summary>
        /// <param name="strCadena"></param>
        public static void CadenaConexion(string strCadena)
        {
            AppSettings.DatosConexion = strCadena;
        }

        /// <summary>
        /// Método que asigna la segunda cadena de conexión a la variable estatica de la clase AppSettings
        /// Segunda base de atos Proximo CRM
        /// </summary>
        /// <param name="strCadena"></param>
        public static void CadenaConexion2(string strCadena)
        {
            AppSettings.DatosConexion2 = strCadena;
        }

        /// <summary>
        /// Método que asigna la ruta dónde se guaradarán todas las imagenes de los artículos
        /// </summary>
        /// <param name="strDirectorioImagenesArticulos"></param>
        public static void DirectorioImagenesArticulos(string strDirectorioImagenesArticulos)
        {
            AppSettings.DirectorioImagenesArticulos = strDirectorioImagenesArticulos;
        }

        /// <summary>
        /// Método que asigna el dominio por el cuál será autorizado utilizar la API
        /// </summary>
        /// <param name="strHostParaCors"></param>
        public static void HostParaCors(string strHostParaCors)
        {
            AppSettings.HostParaCors = strHostParaCors;
        }

        /// <summary>
        /// Método que asigna el dominio por el cuál será autorizado utilizar la API
        /// </summary>
        /// <param name="strHostParaCors"></param>
        public static void HostParaCors2(string strHostParaCors)
        {
            AppSettings.HostParaCors2 = strHostParaCors;
        }

        /// <summary>
        /// Método que asigna el directorio raíz donde corre las vistas publicas
        /// </summary>
        /// <param name="strDirectorioRoot"></param>
        public static void DirectorioRoot(string strDirectorioRoot)
        {
            AppSettings.DirectorioRoot = strDirectorioRoot;
        }

        /// <summary>
        /// Método que asigna la cadena secreta para los token JWT
        /// </summary>
        /// <param name="strSecretToken"></param>
        public static void SecretToken(string strSecretToken)
        {
            AppSettings.Secret = strSecretToken;
        }

        /// <summary>
        /// Método que asigna el usuario para la carpeta compartida donde estan las imagenes de los productos
        /// </summary>
        /// <param name="strUsuarioImagenes"></param>
        public static void UsuarioCarpetaImagenes(string strUsuarioImagenes)
        {
            AppSettings.UsuarioImagenes = strUsuarioImagenes;
        }

        /// <summary>
        /// Método que asigna la contraseña para la carpeta compartida donde estan las imagenes de los productos
        /// </summary>
        /// <param name="strContrasenaImagenes"></param>
        public static void ConstrasenaCarpetaImagenes(string strContrasenaImagenes)
        {
            AppSettings.ContrasenaImagenes = strContrasenaImagenes;
        }

        /// <summary>
        /// Método que asigna la dirección de la URL donde se trabajará con la API
        /// </summary>
        /// <param name="strUrlAPI"></param>
        public static void DireccionURLAPI(string strUrlAPI)
        {
            AppSettings.UrlAPI = strUrlAPI;
        }

        /// <summary>
        /// Método que asigna la dirección de la carpeta compartida donde se almacenan las Facturas
        /// </summary>
        /// <param name="strRutaAnexos"></param>
        public static void DireccionRutaAnexos(string strRutaAnexos)
        {
            AppSettings.RutaAnexos = strRutaAnexos;
        }

        /// <summary>
        /// Método que asigna el usuario de la carpeta compartida donde se almacenan las Facturas
        /// </summary>
        /// <param name="strUsuarioAnexos"></param>
        public static void CredencialesUsuarioAnexos(string strUsuarioAnexos)
        {
            AppSettings.UsuarioAnexos = strUsuarioAnexos;
        }

        /// <summary>
        /// Método que asigna la contraseña de la carpeta compartida donde se almacenan las Facturas
        /// </summary>
        /// <param name="strPassAnexos"></param>
        public static void CredencialesPassAnexos(string strPassAnexos)
        {
            AppSettings.PassAnexos = strPassAnexos;
        }

        /// <summary>
        /// Método que asigna la ruta de las evidencias de fotografias tomadas por chofer a la variable global
        /// </summary>
        /// <param name="pathEvidencias"></param>
        public static void DirectorioEvidencias(KeyValueModel pathEvidencias)
        {
            AppSettings.PathEvicendias = pathEvidencias;
        }

        /// <summary>
        /// Método que asigna la ruta del direcorio de logisitca para firmas y documentos
        /// </summary>
        /// <param name="rutaLogistica"></param>
        public static void DirectorioLogistica(string rutaLogistica)
        {
            AppSettings.RutaLogistica = rutaLogistica;
        }

        public static void CredencialesUsuarioLogistica(string strUsuarioLogistica)
        {
            AppSettings.UsuarioLogistica = strUsuarioLogistica;
        }

        /// <summary>
        /// Método que asigna la contraseña de la carpeta compartida donde se almacenan las Facturas
        /// </summary>
        /// <param name="strPassAnexos"></param>
        public static void CredencialesPassLogisticas(string strPassLogistica)
        {
            AppSettings.PassLogistica = strPassLogistica;
        }

        public static void DirectorioLogosSubdistribuidor(string pathLogos)
        {
            AppSettings.PathLogosSubdistribuidor = pathLogos;
        }

        public static void TokenEpiresInDays(int value)
        {
            AppSettings.TokenExpiresInDays = value;
        }

        public static void TokenEpiresInDaysAppSD(int value)
        {
            AppSettings.TokenExpiresInDaysAppSD = value;
        }

        public static void DestinatariosChatBot(string value)
        {
            AppSettings.DestinatariosChatBot = value;
        }


        public static void RutaAnexosControlCalidad(KeyValueModel value)
        {
            AppSettings.RutaAnexosControlCalidad = value;
        }

        #endregion Acción
    }
}