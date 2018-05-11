using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.DbManager;

namespace HotelApi
{
    public class HotelRepository : IRepository<Hotel>
    {
        private HotelContext context = new HotelContextFactory().CreateDbContext();
        public Hotel Add(Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel Get(Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel Update(Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel Remove(Hotel obj)
        {
            throw new NotImplementedException();
        }

        public List<Hotel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Hotel> Search(Hotel obj)
        {
            throw new NotImplementedException();
        }

        public bool Reseed()
        {
            throw new NotImplementedException();
        }
    }
}
