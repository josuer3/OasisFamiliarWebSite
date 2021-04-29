using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Model.DataModel
{
    public class ContextDB : DbContext
    {
        public ContextDB(): base ("OasisFamiliarDB")
        {
        
        }

        public DbSet<Comprado> Comprado { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        
        public DbSet<Promociones> Promociones { get; set; }
    }
}
