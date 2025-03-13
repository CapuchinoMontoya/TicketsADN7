using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTELISIS.APPCORE.EL
{
    public partial class UsuarioPortal 
    {
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }
        [Display(Name = "Puesto")]
        public string Rol { get; set; }
        [Display(Name = "Foto de perfil")]
        public string ImagenDePerfil { get; set; }
        [Display(Name = "Empresa")]
        public string Empresa { get; set; }
        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        [Display(Name = "Telefono(s)")]
        public string Telefono { get; set; }
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Display(Name = "Acerca de mi")]
        public string Descripcion { get; set; }
        
    }
}