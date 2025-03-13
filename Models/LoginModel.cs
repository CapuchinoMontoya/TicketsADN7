using System.ComponentModel.DataAnnotations;

namespace TicketsADN7.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Ingrese su número de agente")]
        public required string username { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        public required string password { get; set; }
    }
}