using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Mexico",
                    ShortName = "MX"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States",
                    ShortName = "US"
                },
                new Country
                {
                    Id = 3,
                    Name = "Canada",
                    ShortName = "CN"
                }
             );

            modelBuilder.Entity<Hotel>().HasData(
       new Hotel
       {
           Id = 1,
           Name = "Hotel 70",
           Address = "Monterrey",
           CountryId = 1,
           Rating = 4.9,
       },
       new Hotel
       {
           Id = 2,
           Name = "Courtyard Royal",
           Address = "Texas",
           CountryId = 2,
           Rating = 4.5
       },
       new Hotel
       {
           Id = 3,
           Name = "Montana Hotel",
           Address = "Montana",
           CountryId = 3,
           Rating = 4.7
       }
    );

        }

    }
}
