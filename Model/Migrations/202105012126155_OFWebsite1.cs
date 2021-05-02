namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OFWebsite1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promociones", "Titulo", c => c.String());
            AlterColumn("dbo.Promociones", "Active", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promociones", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Promociones", "Titulo");
        }
    }
}
