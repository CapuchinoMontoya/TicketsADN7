﻿using INTELISIS.APPCORE.EL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTELISIS.APPCORE.EL
{
    [Table("Departamentos")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartamentoID { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre del departamento no puede exceder 100 caracteres")]
        public string NombreDepartamento { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        public string Descripcion { get; set; }

        // Propiedad de navegación
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}