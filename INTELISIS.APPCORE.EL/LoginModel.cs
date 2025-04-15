using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Ingrese su usuario")]
        public required string username { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        public required string password { get; set; }
    }
    public class LoginResponse
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }

}