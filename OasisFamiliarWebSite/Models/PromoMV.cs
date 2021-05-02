using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OasisFamiliarWebSite.Models
{
    public class PromoMV
    {

        [Key]
        public int idPromociones { get; set; }


        [Required]
        [Display(Name = "Primer dia")]
        public DateTime fechaInicio { get; set; }
        [Required]
        [Display(Name = "Ultimo dia")]
        public DateTime fechaFin { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Promocion")]
        public bool Activar { get; set; }


    }
}