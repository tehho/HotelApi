using System.Linq;
using Hotel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hotel.Infrastructure.DbManager
{
    public interface IDbManager
    {
        IQueryable<HotelRegion> HotelRegions { get; set; }
        IQueryable<Domain.Hotel> Hotels { get; set; }

        DatabaseFacade Database { get; }

        int SaveChanges();
    }
}