using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OasisFamiliarWebSite.Models
{
    public class CustomerVM
    {
        [Key]
        public int idUsuario { get; set; }
        [Display(Name = "Usuario")]
        public string Nombre_Usuario { get; set; }
        public int EstadoCuenta { get; set; }
        public int FacturaActual { get; set; }
        public double MontoActual { get; set; }

    }
}