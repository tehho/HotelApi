using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HotelApi.DbManager
{
    public class HotelContextFactory : IDesignTimeDbContextFactory<HotelContext>
    {
        private static string _connectionString = null;

        private static HotelContext _instance = new HotelContextFactory().CreateDbContext();
        public static HotelContext SingleInstance => _instance;

        public HotelContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public HotelContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<HotelContext>();
            builder.UseSqlServer(_connectionString);

            return new HotelContext(builder.Options);
        }

        private void LoadConnectionString()
        {
            if (File.Exists("appsettings.json"))
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);

                var configuration = builder.Build();

                _connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = "Server = (localdb)\\mssqllocaldb ; Database = db_hotelapi_andreas_joakim; Trusted_Connection = true;";
                }

            }
        }
    }

}