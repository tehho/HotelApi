using System;
using System.Collections.Generic;
using Hotel.Infrastructure.DbManager;

namespace Hotel.Infrastructure.Repository
{
    public class HotelRepository : IRepository<Hotel.Domain.Hotel>
    {
        private HotelContext _context = new HotelContextFactory().CreateDbContext();

        public Hotel.Domain.Hotel Add(Hotel.Domain.Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel.Domain.Hotel Get(Hotel.Domain.Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel.Domain.Hotel Update(Hotel.Domain.Hotel obj)
        {
            throw new NotImplementedException();
        }

        public Hotel.Domain.Hotel Remove(Hotel.Domain.Hotel obj)
        {
            throw new NotImplementedException();
        }

        public List<Hotel.Domain.Hotel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Hotel.Domain.Hotel> Search(Hotel.Domain.Hotel obj)
        {
            throw new NotImplementedException();
        }

        public bool Reseed()
        {
            throw new NotImplementedException();
        }
    }
}
