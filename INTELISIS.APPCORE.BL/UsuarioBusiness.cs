using INTELISIS.APPCORE.EL;
using INTELISIS.APPCORE.DL;

namespace INTELISIS.APPCORE.BL
{
    /// <summary>
    /// Clase que administra a los usuarios
    /// </summary>
    public class UsuarioBusiness
    {
        #region Consultar

        /// <summary>
        /// Método para iniciar sesion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Usuario IniciarSesion(LoginModel model)
        {
            return UsuarioDataAccess.IniciarSesion(model);
        }

        #endregion Acción
    }
}