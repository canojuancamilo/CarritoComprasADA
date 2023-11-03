namespace CarritoCompra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaProducto",
                c => new
                    {
                        id_categoria = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id_categoria);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        id_pedido = c.Int(nullable: false, identity: true),
                        id_producto = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                        fecha_pedido = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_pedido)
                .ForeignKey("dbo.Producto", t => t.id_producto, cascadeDelete: true)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        id_producto = c.Int(nullable: false, identity: true),
                        id_categoria = c.Int(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 150),
                        CantidadDisponible = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id_producto)
                .ForeignKey("dbo.CategoriaProducto", t => t.id_categoria, cascadeDelete: true)
                .Index(t => t.id_categoria);
            
            CreateTable(
                "dbo.Transaccion",
                c => new
                    {
                        id_transaccion = c.Int(nullable: false, identity: true),
                        id_usuario = c.Int(nullable: false),
                        fecha_transaccion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_transaccion)
                .ForeignKey("dbo.Usuario", t => t.id_usuario, cascadeDelete: true)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id_usuario = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 150),
                        identificacion = c.String(nullable: false, maxLength: 20),
                        direccion = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(nullable: false, maxLength: 20),
                        usuario = c.String(nullable: false, maxLength: 20),
                        contrasena = c.String(nullable: false, maxLength: 20),
                        id_perfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_usuario)
                .ForeignKey("dbo.Perfil", t => t.id_perfil, cascadeDelete: true)
                .Index(t => t.id_perfil);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaccion", "id_usuario", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Pedido", "id_producto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "id_categoria", "dbo.CategoriaProducto");
            DropIndex("dbo.Usuario", new[] { "id_perfil" });
            DropIndex("dbo.Transaccion", new[] { "id_usuario" });
            DropIndex("dbo.Producto", new[] { "id_categoria" });
            DropIndex("dbo.Pedido", new[] { "id_producto" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Transaccion");
            DropTable("dbo.Producto");
            DropTable("dbo.Pedido");
            DropTable("dbo.CategoriaProducto");
        }
    }
}
