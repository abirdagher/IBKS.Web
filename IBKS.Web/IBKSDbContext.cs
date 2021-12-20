using IBKS.Web.Models;
using Microsoft.EntityFrameworkCore;
using Type = IBKS.Web.Models.Type;

namespace IBKS.Web
{
    public class IBKSDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Light> Lights { get; set; }

        public IBKSDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>()
           .HasData(
             new Type { Id = 1, Name = "Type 1" },
             new Type { Id = 2, Name = "Type 2" },
             new Type { Id = 3, Name = "Type 3" },
             new Type { Id = 4, Name = "Type 4" }
           );

            modelBuilder.Entity<Room>()
           .HasData(
             new Room { Id = 1, Name = "Room 1" },
             new Room { Id = 2, Name = "Room 2" },
             new Room { Id = 3, Name = "Room 3" },
             new Room { Id = 4, Name = "Room 4" }
           );

            modelBuilder.Entity<Light>()
          .HasData(
            new Light { Id = 1, Name = "Light 1", Description = "Description 1", RoomId = 1, TypeId = 1 },
            new Light { Id = 2, Name = "Light 2", Description = "Description 2", RoomId = 2, TypeId = 2 },
            new Light { Id = 3, Name = "Light 3", Description = "Description 3", RoomId = 3, TypeId = 3 },
            new Light { Id = 4, Name = "Light 4", Description = "Description 4", RoomId = 4, TypeId = 4 }
          );
        }
      
        public IBKSDbContext(DbContextOptions<IBKSDbContext> options)
            : base(options)
        {

        }
    }
}
