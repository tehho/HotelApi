using Hotel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hotel.Infrastructure.DbManager
{
    public interface IDbManager
    {
        DbSet<HotelRegion> HotelRegions { get; set; }
        DbSet<Domain.Hotel> Hotels { get; set; }

        DatabaseFacade Database { get; }

        int SaveChanges();
    }
}