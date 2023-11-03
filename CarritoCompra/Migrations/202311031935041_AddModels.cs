namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        id_perfil = c.Int(nullable: false, identity: true),
                        rol = c.String(),
                    })
                .PrimaryKey(t => t.id_perfil);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Perfil");
        }
    }
}
