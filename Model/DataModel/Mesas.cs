using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Mesas
    {
        [Key]
        public int idMesa { get; set; }
        [Display(Name = "Mesas")]
        public string Nombre_Mesa { get; set; }
        public bool Disponible { get; set; }
        
    }
}
