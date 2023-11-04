namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Producto", "id_categoria", "dbo.CategoriaProducto");
            DropForeignKey("dbo.Usuario", "id_perfil", "dbo.Perfil");
            DropPrimaryKey("dbo.CategoriaProducto");
            DropPrimaryKey("dbo.Perfil");
            AlterColumn("dbo.CategoriaProducto", "id_categoria", c => c.Int(nullable: false));
            AlterColumn("dbo.Perfil", "id_perfil", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CategoriaProducto", "id_categoria");
            AddPrimaryKey("dbo.Perfil", "id_perfil");
            AddForeignKey("dbo.Producto", "id_categoria", "dbo.CategoriaProducto", "id_categoria", cascadeDelete: true);
            AddForeignKey("dbo.Usuario", "id_perfil", "dbo.Perfil", "id_perfil", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Producto", "id_categoria", "dbo.CategoriaProducto");
            DropPrimaryKey("dbo.Perfil");
            DropPrimaryKey("dbo.CategoriaProducto");
            AlterColumn("dbo.Perfil", "id_perfil", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CategoriaProducto", "id_categoria", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Perfil", "id_perfil");
            AddPrimaryKey("dbo.CategoriaProducto", "id_categoria");
            AddForeignKey("dbo.Usuario", "id_perfil", "dbo.Perfil", "id_perfil", cascadeDelete: true);
            AddForeignKey("dbo.Producto", "id_categoria", "dbo.CategoriaProducto", "id_categoria", cascadeDelete: true);
        }
    }
}
