using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Password { get; set; }
        public string Posicion { get; set; }

    }
}
