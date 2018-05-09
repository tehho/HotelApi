using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelApi.DbManager
{
    public class HotelContextFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        private static string _connectionString = null;

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

            var builder = new DbContextOptionsBuilder<CustomerContext>();
            builder.UseSqlServer(_connectionString);

            return new CustomerContext(builder.Options);
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
            else
            {
                //TODO Endast för oskar
                _connectionString = "Server = (localdb)\\mssqllocaldb ; Database = db_krm; Trusted_Connection = true;";
            }
        }
    }

}