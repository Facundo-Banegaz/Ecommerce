using Ecommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)  : base(options)
        {    
        }


        public DbSet<Country> Countries { get; set; }

        public DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Category>().HasIndex(c =>c.Name).IsUnique();

        }


    }
}
