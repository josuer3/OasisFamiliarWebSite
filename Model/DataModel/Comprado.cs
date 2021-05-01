using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.DataModel
{
    public class Comprado
    {

        [Key]
        public int idComprado { get; set; }
        public int idFactura { get; set; }
        public int idMenu { get; set; }
        public int Cantidad { get; set; }
        public int estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
