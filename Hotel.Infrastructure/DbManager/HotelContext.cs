using System.Linq;
using Hotel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.DbManager
{
    public class HotelContext : DbContext, IDbManager
    {
        public static HotelContext Instance { get; } = new HotelContextFactory().CreateDbContext();

        public DbSet<HotelRegion> HotelRegions { get; set; }
        public DbSet<Domain.Hotel> Hotels { get; set; }
        IQueryable<HotelRegion> IDbManager.HotelRegions { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        IQueryable<Domain.Hotel> IDbManager.Hotels { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
    }
}
