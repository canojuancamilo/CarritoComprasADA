namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producto", "url");
        }
    }
}
