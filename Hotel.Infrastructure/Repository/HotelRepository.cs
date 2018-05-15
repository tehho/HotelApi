using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.Infrastructure.DbManager;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class HotelRepository : IRepository<Domain.Hotel>
    {
        private readonly HotelContext _context = new HotelContextFactory().CreateDbContext();

        public Domain.Hotel Add(Domain.Hotel obj)
        {
            _context.Hotels.Add(obj);
            return obj;
        }

        public Domain.Hotel Get(Domain.Hotel obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (obj.Id != null)
                return SearchOne(hotel => hotel.Id == obj.Id);

            if (obj.Name != null)
                return SearchOne(hotel => hotel.Name == obj.Name);

            throw new ArgumentException();
        }

        public Domain.Hotel Update(Domain.Hotel obj)
        {
            var hotel = Get(obj);
            if (obj.Name != null)
            {
                hotel.Name = obj.Name;
            }
            
            if (obj.HotelRegionId != null)
            {
                hotel.HotelRegionId = obj.HotelRegionId;
            }

            if (obj.RoomsAvailable != null)
            {
                hotel.RoomsAvailable = obj.RoomsAvailable;
            }

            _context.SaveChanges();

            return hotel;
        }

        public Domain.Hotel Remove(Domain.Hotel obj)
        {
            var hotel = Get(obj);

            _context.Remove(hotel);

            return hotel;
        }

        public List<Domain.Hotel> GetAll()
        {
            return SearchList(hotel => true).ToList();
        }

        public List<Domain.Hotel> Search(Domain.Hotel obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (obj.HotelRegionId != null)
                return SearchList(hotel => hotel.HotelRegionId == obj.HotelRegionId).ToList();

            if (obj.RoomsAvailable != null)
                return SearchList(hotel => hotel.RoomsAvailable > obj.RoomsAvailable).ToList();

            if (obj.Name != null)
                return SearchList(hotel => hotel.Name.Contains(obj.Name)).ToList();

            return GetAll();
        }

        public bool Reseed()
        {
            try
            {
                var list = _context.Hotels.ToList();
                _context.Hotels.RemoveRange(list);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Domain.Hotel SearchOne(Func<Domain.Hotel, bool> method)
        {
            return _context.Hotels.Single(method);
        }
        public IEnumerable<Domain.Hotel> SearchList(Func<Domain.Hotel, bool> method)
        {
            return _context.Hotels.Include(hotel => hotel.Region).Where(method).ToList();
        }
      
    }
}
