using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool premio { get; set; }
        
        //llave
        public int idRol { get; set; }
        [ForeignKey("idRol")]
        public virtual Rol Role { get; set; }

    }
}
