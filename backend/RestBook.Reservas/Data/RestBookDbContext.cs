using Microsoft.EntityFrameworkCore;
using RestBook.Reservas.Models;

namespace RestBook.Reservas.Data
{
    public class RestBookDbContext : DbContext
    {
        public RestBookDbContext(DbContextOptions<RestBookDbContext> options)
            : base(options) { }

        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Mesa> Mesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mesa>()
                .HasMany(m => m.Reservas)
                .WithOne(r => r.Mesa!)
                .HasForeignKey(r => r.MesaId);
        }
    }
}
