using Hotel.Domain;
using Hotel.Infrastructure.DbManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hotel.Infrastructure.Test
{
    public class MockDbManager : IDbManager
    {
        public DbSet<HotelRegion> HotelRegions { get; set; }
        public DbSet<Domain.Hotel> Hotels { get; set; }
        public DatabaseFacade Database { get; }
        public int SaveChanges()
        {
           return 0;
        }
    }
}
