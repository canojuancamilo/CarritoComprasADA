namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "id_transaccion", c => c.Int(nullable: false));
            CreateIndex("dbo.Pedido", "id_transaccion");
            AddForeignKey("dbo.Pedido", "id_transaccion", "dbo.Transaccion", "id_transaccion", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "id_transaccion", "dbo.Transaccion");
            DropIndex("dbo.Pedido", new[] { "id_transaccion" });
            DropColumn("dbo.Pedido", "id_transaccion");
        }
    }
}
