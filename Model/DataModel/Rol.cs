﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Rol
    {
        [Key]
        public int idRol { get; set; }
        [Required]
        public string Nombre_Rol { get; set; }
        [Required]
        [StringLength(300)]
        public string Descripcion { get; set; }
    }
}
