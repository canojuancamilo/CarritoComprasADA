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

        public virtual DbSet<Perfil> Perfil { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Perfil>();
        }
    }
}
