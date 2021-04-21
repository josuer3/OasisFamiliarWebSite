using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OasisFamiliarWebSite.Models
{
    public class DetallesVM
    {
        [Key]
        public int idComprado { get; set; }
        public int idFactura { get; set; }
        public int idMenu { get; set; }
        public int Cantidad { get; set; }
        public int producto { get; set; }
        public int precio { get; set; }

    }
}