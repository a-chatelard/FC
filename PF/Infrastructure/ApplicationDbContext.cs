using Microsoft.EntityFrameworkCore;
using PF.Domain.Entities;

namespace PF.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context): base(context)
        {
            
        }

        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Facture> Factures { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Client>().HasMany(c => c.Factures).WithOne()
                .HasForeignKey(f => f.ClientId);

            modelBuilder.Entity<Facture>().HasKey(f => f.Id);
        }
    }
}
