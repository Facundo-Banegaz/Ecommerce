using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Data
{
    public class EcommerceContext : DbContext
    {
        public  EcommerceContext(DbContextOptions<EcommerceContext> options) : base (options)
         
        {
        
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<ProductoImagen> ProductoImagens { get; set; }
        public virtual DbSet<Categoria> Categorias  { get; set; }
        public virtual DbSet<Orden>Ordenes { get; set; }
        public virtual DbSet<OrdenProducto> OrdenProductos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Ciudad> Ciudades { get; set; }
        //public virtual DbSet<Comprobante>  Comprobantes{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 


        }
    }
}
