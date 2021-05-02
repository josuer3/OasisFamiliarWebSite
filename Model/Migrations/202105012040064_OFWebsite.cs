namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OFWebsite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compradoes",
                c => new
                    {
                        idComprado = c.Int(nullable: false, identity: true),
                        idFactura = c.Int(nullable: false),
                        idMenu = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idComprado);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        idFactura = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        idCliente = c.Int(nullable: false),
                        idVendedor = c.Int(nullable: false),
                        idMesa = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idFactura);
            
            CreateTable(
                "dbo.Inventarios",
                c => new
                    {
                        idInventario = c.Int(nullable: false, identity: true),
                        Proveedor = c.String(),
                        Telefono = c.Int(nullable: false),
                        Existencias = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idInventario);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        idItem = c.Int(nullable: false, identity: true),
                        idMenu = c.Int(nullable: false),
                        idInventario = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idItem);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        idMenu = c.Int(nullable: false, identity: true),
                        nombre_Producto = c.String(),
                        Precio = c.Double(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Promocion = c.String(),
                        Tipo_Producto = c.String(),
                    })
                .PrimaryKey(t => t.idMenu);
            
            CreateTable(
                "dbo.Mesas",
                c => new
                    {
                        idMesa = c.Int(nullable: false, identity: true),
                        Nombre_Mesa = c.String(),
                        Disponible = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idMesa);
            
            CreateTable(
                "dbo.Promociones",
                c => new
                    {
                        idPromociones = c.Int(nullable: false, identity: true),
                        fechaInicio = c.DateTime(nullable: false),
                        fechaFin = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idPromociones);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        idRol = c.Int(nullable: false, identity: true),
                        Nombre_Rol = c.String(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.idRol);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        Nombre_Usuario = c.String(),
                        Password = c.String(),
                        Promociones = c.Boolean(nullable: false),
                        Correo = c.String(),
                        idRol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idUsuario)
                .ForeignKey("dbo.Rols", t => t.idRol, cascadeDelete: true)
                .Index(t => t.idRol);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "idRol", "dbo.Rols");
            DropIndex("dbo.Usuarios", new[] { "idRol" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Rols");
            DropTable("dbo.Promociones");
            DropTable("dbo.Mesas");
            DropTable("dbo.Menus");
            DropTable("dbo.Items");
            DropTable("dbo.Inventarios");
            DropTable("dbo.Facturas");
            DropTable("dbo.Compradoes");
        }
    }
}
