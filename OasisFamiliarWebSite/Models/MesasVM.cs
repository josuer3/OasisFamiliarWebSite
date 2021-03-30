using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OasisFamiliarWebSite.Models
{
    public class MesasVM
    {
        [Key]
        public int idMesa { get; set; }
        [Display(Name = "Mesas")]
        public string Nombre_Mesa { get; set; }
        public int Disponible { get; set; }
        public string Nombre_Cliente { get; set; }
    }
}
