using Course_Test_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Course_Test_Task
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>()
                .HasOne(c => c.ParentCatalog)
                .WithMany(c => c.SubCatalogs)
                .HasForeignKey(c => c.ParentCatalogId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
