using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Promociones
    {

        [Key]
        public int idPromociones { get; set; }
        [Required]
        public DateTime fechaInicio { get; set; }
        [Required]
        public DateTime fechaFin { get; set; }
        public string Descripcion { get; set; }
        public bool Active { get; set; }

    }
}
