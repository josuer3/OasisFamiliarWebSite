namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Promociones", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuarios", "Correo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Correo");
            DropColumn("dbo.Usuarios", "Promociones");
        }
    }
}
