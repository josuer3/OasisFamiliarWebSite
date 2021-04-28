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
        public DateTime fechaInicio { get; set; }
        [Required]
        public DateTime fechaFin { get; set; }
        public string Descripcion { get; set; }
        public bool Active { get; set; }


    }
}