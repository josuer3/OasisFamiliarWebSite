namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Model.DataModel;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.DataModel.ContextDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.DataModel.ContextDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //Se agrega información basica para BD de Datos
            //Esta información solo sirve para tener datos en el Sistema, mientras se crea                       
            context.Menu.AddOrUpdate(x => x.idMenu,
                new Menu() { idMenu = 1, nombre_Producto = "Balde de Pilsener", Precio = 6.00, Estado = true, Tipo_Producto = "Bebida" },
                new Menu() { idMenu = 2, nombre_Producto = "Pilsener", Precio = 0.75, Estado = true, Tipo_Producto = "Bebida" },
                new Menu() { idMenu = 3, nombre_Producto = "Balde de Golden", Precio = 6.00, Estado = true, Tipo_Producto = "Bebida" },
                new Menu() { idMenu = 4, nombre_Producto = "Sopa de tortilla (Boca)", Precio = 1.00, Estado = true, Tipo_Producto = "Boca" },
                new Menu() { idMenu = 5, nombre_Producto = "Sopa de tortilla", Precio = 6.00, Estado = true, Tipo_Producto = "Comida" },
                new Menu() { idMenu = 6, nombre_Producto = "Carne Asada", Precio = 6.00, Estado = true, Tipo_Producto = "Comida" }
            );

            //Se agrega informacion de las mesas existentes
            //Seran 10 mesas, 7 disponibles, 2 ocupadas, 1 reservada, 1 desabilitada
            context.Mesas.AddOrUpdate(x => x.idMesa,
                new Mesas() { idMesa = 1, Nombre_Mesa = "Mesa 1", Disponible = 0 },
                new Mesas() { idMesa = 2, Nombre_Mesa = "Mesa 2", Disponible = 0 },
                new Mesas() { idMesa = 3, Nombre_Mesa = "Mesa 3", Disponible = 0 },
                new Mesas() { idMesa = 4, Nombre_Mesa = "Mesa 4", Disponible = 0 },
                new Mesas() { idMesa = 5, Nombre_Mesa = "Mesa 5", Disponible = 0 },
                new Mesas() { idMesa = 6, Nombre_Mesa = "Mesa 6", Disponible = 0 },
                new Mesas() { idMesa = 7, Nombre_Mesa = "Mesa 7", Disponible = 0 },
                new Mesas() { idMesa = 8, Nombre_Mesa = "Mesa 8", Disponible = 1 },
                new Mesas() { idMesa = 9, Nombre_Mesa = "Mesa 9", Disponible = 1 },
                new Mesas() { idMesa = 10, Nombre_Mesa = "Mesa 10", Disponible = 2 },
                new Mesas() { idMesa = 11, Nombre_Mesa = "Mesa 11", Disponible = 2 }
            );


            //Se agrega informacion de las mesas existentes
            //Seran roles
            context.Rol.AddOrUpdate(x => x.idRol,
                new Rol() { idRol = 1, Nombre_Rol = "Cliente", Descripcion = "Cliente" },
                new Rol() { idRol = 2, Nombre_Rol = "Mesero", Descripcion = "Mesero" },
                new Rol() { idRol = 3, Nombre_Rol = "Cocinero", Descripcion = "Cocinero" },
                new Rol() { idRol = 4, Nombre_Rol = "Admin", Descripcion = "Admin" }
            );



            //Se agrega informacion de las mesas existentes
            //Seran 4 usuario --- password = abd12345
            context.Usuario.AddOrUpdate(x => x.idUsuario,
                new Usuario() { idUsuario = 1, Nombre_Usuario = "Mesa1", Password = "ABJBix1Jh4p0eRSOu3F3oWW4A1ZWndTAk0l1Q4+zfFCQKMws4ZuzY/uevTiHb61tSA==", idRol = 1 },
                new Usuario() { idUsuario = 2, Nombre_Usuario = "Mesa2", Password = "ABJBix1Jh4p0eRSOu3F3oWW4A1ZWndTAk0l1Q4+zfFCQKMws4ZuzY/uevTiHb61tSA==", idRol = 2 },
                new Usuario() { idUsuario = 3, Nombre_Usuario = "Mesa3", Password = "ABJBix1Jh4p0eRSOu3F3oWW4A1ZWndTAk0l1Q4+zfFCQKMws4ZuzY/uevTiHb61tSA==", idRol = 3 },
                new Usuario() { idUsuario = 4, Nombre_Usuario = "Mesa4", Password = "ABJBix1Jh4p0eRSOu3F3oWW4A1ZWndTAk0l1Q4+zfFCQKMws4ZuzY/uevTiHb61tSA==", idRol = 4 },
                new Usuario() { idUsuario = 5, Nombre_Usuario = "Isa", Password = "ABJBix1Jh4p0eRSOu3F3oWW4A1ZWndTAk0l1Q4+zfFCQKMws4ZuzY/uevTiHb61tSA==", idRol = 4 }
            );


        }
    }
}
