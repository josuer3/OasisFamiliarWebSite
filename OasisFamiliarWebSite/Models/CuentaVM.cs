using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OasisFamiliarWebSite.Models
{
    public class CuentaVM
    {
        [Key]
        public int idUsuario { get; set; }
        [Display(Name = "Nombre de Usuario")]
        public string Nombre_Usuario { get; set; }
        public string Password { get; set; }
        public bool Promociones { get; set; }
        public string Correo { get; set; }
    }
}