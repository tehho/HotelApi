using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.Domain;
using Hotel.Infrastructure.DbManager;

namespace Hotel.Infrastructure.Repository
{
    public class HotelRegionRepository : IRepository<HotelRegion>
    {
        private HotelContext context = new HotelContextFactory().CreateDbContext();

        public HotelRegion Add(HotelRegion obj)
        {
            HotelRegion temp = new HotelRegion(obj);

            if (obj.Id != null)
            {
                //TODO: Lägg till update när id finns i db redan
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.HotelRegions ON");

                    context.HotelRegions.Add(temp);
                    context.SaveChanges();

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.HotelRegions OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
            else
            {
                context.HotelRegions.Add(temp);
                context.SaveChanges();
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

            context.SaveChanges();

            return temp;
        }

        public HotelRegion Remove(HotelRegion obj)
        {
            var temp = Get(obj);

            context.HotelRegions.Remove(temp);
            context.SaveChanges();


            return temp;
        }

        public List<HotelRegion> GetAll()
        {
            List<HotelRegion> list = null;

            list = context.HotelRegions.ToList();

            return list;
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
                var list = context.HotelRegions.ToList();
                context.RemoveRange(list);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private HotelRegion SearchSingle(Func<HotelRegion, bool> method)
        {
            return context.HotelRegions.Include(hotelRegion => hotelRegion.Hotels).Single(method);
        }

        private IEnumerable<HotelRegion> SearchList(Func<HotelRegion, bool> method)
        {
            return context.HotelRegions.Include(hotelRegion => hotelRegion.Hotels).Where(method);
        }

    }
}