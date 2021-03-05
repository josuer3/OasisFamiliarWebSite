using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OasisFamiliarWebSite.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [Remote("VerifyIfUserExist", "Account", HttpMethod = "POST", ErrorMessage = "El Nombre De Usuario Ya Existe ")]
        public string Nombre_Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

       
        public string Posicion { get; set; }

        
    }
}