using Hotel.Domain;

namespace Hotel.Infrastructure.DbManager
{
    public class HotelContext : DbContext
    {
        public static HotelContext Instance { get; } = new HotelContextFactory().CreateDbContext();

        public DbSet<HotelRegion> HotelRegions { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }
    }
}
