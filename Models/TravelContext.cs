using Microsoft.EntityFrameworkCore;

namespace Travel.Models
{
    public class TravelContext : DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
          builder.Entity<Place>()
              .HasData(
                  new Place { PlaceId = 1, Name = "Venice", Country = "Italy", Rating = 3 },
                  new Place { PlaceId = 2, Name = "Rome", Country = "Italy", Rating = 5 },
                  new Place { PlaceId = 3, Name = "Toronto", Country = "Canada", Rating = 2 },
                  new Place { PlaceId = 4, Name = "Toledo", Country = "United States", Rating = 4 },
                  new Place { PlaceId = 5, Name = "Buffalo", Country = "United States", Rating = 2 }
              );
        }
        public DbSet<Place> Places { get; set; }
    }
}