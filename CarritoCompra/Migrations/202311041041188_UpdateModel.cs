namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "Cantidad_disponible", c => c.Int(nullable: false));
            DropColumn("dbo.Producto", "CantidadDisponible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Producto", "CantidadDisponible", c => c.Int(nullable: false));
            DropColumn("dbo.Producto", "Cantidad_disponible");
        }
    }
}
