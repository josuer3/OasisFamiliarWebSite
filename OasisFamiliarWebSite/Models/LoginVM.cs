
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OasisFamiliarWebSite.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El Nombre de Usuario es requerido!")]
        public string UUsuario { get; set; }


        [Required(ErrorMessage = "La Contrasena es requerida!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}