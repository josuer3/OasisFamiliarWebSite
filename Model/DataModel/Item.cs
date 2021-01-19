using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Item
    {
        [Key]
        public int idItem { get; set; }
        public int idMenu { get; set; }
        public int idInventario { get; set; }
        public int Cantidad { get; set; }

    }
}
