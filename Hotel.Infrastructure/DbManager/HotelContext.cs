using Hotel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.DbManager
{
    public class HotelContext : DbContext, IDbManager
    {
        public static HotelContext Instance { get; } = new HotelContextFactory().CreateDbContext();

        public DbSet<HotelRegion> HotelRegions { get; set; }
        public DbSet<Domain.Hotel> Hotels { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
    }
}
