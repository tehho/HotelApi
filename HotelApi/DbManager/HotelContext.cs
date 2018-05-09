using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DbManager
{
    public class HotelContext : DbContext
    {
        public static HotelContext Instance { get; } = new HotelContextFactory().CreateDbContext();

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }

        public DbSet<HotelRegion> HotelRegions { get; set; }
    }
}
