using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            //model.Entity<Brand>()
            //   .HasMany(brand => brand.Products)
            //   .WithOne(product => product.Brand)
            //   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
