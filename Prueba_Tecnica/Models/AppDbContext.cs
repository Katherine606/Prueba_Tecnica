using Microsoft.EntityFrameworkCore;

namespace Prueba_Tecnica.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


            public DbSet<User> Users { get; set; }
            public DbSet<Room> Rooms { get; set; }
            public DbSet<Reservation> Reservations { get; set; }

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Relación User -> Reservations (1 a N)
                modelBuilder.Entity<Reservation>()
                    .HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación Room -> Reservations (1 a N)
                modelBuilder.Entity<Reservation>()
                    .HasOne(r => r.Room)
                    .WithMany(room => room.Reservations)
                    .HasForeignKey(r => r.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }

}