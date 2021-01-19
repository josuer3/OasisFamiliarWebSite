using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Menu
    {
        [Key]
        public int idMenu { get; set; }
        public string nombre_Producto { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }
        public string Promocion { get; set; }
        public string Tipo_Producto { get; set; }
    }
}
