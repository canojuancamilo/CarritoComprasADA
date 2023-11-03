using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CarritoCompra.Models
{
    public class BDContextApplication : DbContext
    {
        public BDContextApplication()
            : base("name=CadenaConexion")
        {
        }

        public virtual DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<CategoriaProducto>();
            modelBuilder.Entity<Pedido>();
            modelBuilder.Entity<Perfil>();
            modelBuilder.Entity<Producto>();
            modelBuilder.Entity<Transaccion>();
            modelBuilder.Entity<Usuario>();
        }
    }
}
