using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OasisFamiliarWebSite.Models
{
    public class DetalleFactura
    {
        [Key]
        public int idComprado { get; set; }
        public int idFactura { get; set; }
        public int idMenu { get; set; }
        public int Cantidad { get; set; }
        public string productoNombre { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public int estado { get; set; }

    }
}