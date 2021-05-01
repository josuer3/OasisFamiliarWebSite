using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OasisFamiliarWebSite.Models
{
    public class CookerVM
    {
        [Key]
        public int idFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int idVendedor { get; set; }
        public int idMesa { get; set; }
        public int estado { get; set; }
        public int Cantidad { get; set; }
        public string nombre_Producto { get; set; }
    }
}