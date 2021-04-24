namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizcionFactu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facturas", "estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Facturas", "estado");
        }
    }
}
