using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.Domain;
using Hotel.Infrastructure.DbManager;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class HotelRegionRepository : IRepository<HotelRegion>
    {
        private readonly HotelContext _context;

        public HotelRegionRepository(HotelContext context)
        {
            _context = context;
        }

        public HotelRegion Add(HotelRegion obj)
        {
            HotelRegion temp = new HotelRegion(obj);

            if (obj.Id != null)
            {
                //TODO: Lägg till update när id finns i db redan
                _context.Database.OpenConnection();
                try
                {
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.HotelRegions ON");

                    _context.HotelRegions.Add(temp);
                    _context.SaveChanges();

                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.HotelRegions OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
            else
            {
                _context.HotelRegions.Add(temp);
                _context.SaveChanges();
            }

            return temp;
        }

        public HotelRegion Get(HotelRegion obj)
        {
            var temp = new HotelRegion(obj);
            try
            {
                if (temp.Id != null)
                {
                    return SearchSingle(region => region.Id == temp.Id);
                }

                if (temp.Name != null)
                {
                    return SearchSingle(region => region.Name == temp.Name);
                }
            }
            catch (InvalidOperationException ioe)
            {
                throw new InvalidOperationException("Failed to search for one element", ioe);
            }

            return temp;
        }

        public HotelRegion Update(HotelRegion obj)
        {
            if (obj.Id == null)
                throw new ArgumentNullException("Update -> HotelRegion", "Id of Region was set to null");

            var temp = Get(obj);

            if (obj.Name != null)
                temp.Name = obj.Name;

            _context.SaveChanges();

            return temp;
        }

        public HotelRegion Remove(HotelRegion obj)
        {
            var temp = Get(obj);

            _context.HotelRegions.Remove(temp);
            _context.SaveChanges();


            return temp;
        }

        public List<HotelRegion> GetAll()
        {
            return SearchList(region => true).ToList();
        }

        public List<HotelRegion> Search(HotelRegion obj)
        {
            if (obj.Id != null)
            {
                return SearchList(region => region.Id == obj.Id).ToList();
            }

            if (obj.Name != null)
            {
                return SearchList(reigon => reigon.Name.Contains(obj.Name)).ToList();
            }

            return GetAll();
        }

        public bool Reseed()
        {
            try
            {
                var list = _context.HotelRegions.ToList();
                _context.HotelRegions.RemoveRange(list);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private HotelRegion SearchSingle(Func<HotelRegion, bool> method)
        {
            return _context.HotelRegions.Include(hotelRegion => hotelRegion.Hotels).Single(method);
        }

        private IEnumerable<HotelRegion> SearchList(Func<HotelRegion, bool> method)
        {
            return _context.HotelRegions.Include(hotelRegion => hotelRegion.Hotels).Where(method).ToList();
        }

    }
}