using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Inventario
    {
        [Key]
        public int idInventario { get; set; }
        public string Proveedor  { get; set; }
        public int Telefono { get; set; }
        public int Existencias { get; set; }
        
    }
}
