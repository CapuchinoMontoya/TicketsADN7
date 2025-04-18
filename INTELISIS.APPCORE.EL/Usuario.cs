﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, ErrorMessage = "La contraseña no puede exceder 255 caracteres")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder 100 caracteres")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [ForeignKey("Rol")]
        public int RolID { get; set; }

        [ForeignKey("Departamento")]
        public int? DepartamentoID { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; }

        //Retorno del sp_Tickets_IniciarSesion
        [NotMapped]
        public string? Resultado { get; set; }
        [NotMapped]
        public string? Mensaje { get; set; }

        // Propiedades de navegación
        public virtual Rol Rol { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}