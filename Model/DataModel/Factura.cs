using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Factura
    {
        [Key]
        public int idFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int idCliente { get; set; }
        public int idVendedor { get; set; }
        public int idMesa { get; set; }

    }
}
