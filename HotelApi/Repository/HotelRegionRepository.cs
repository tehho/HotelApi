using System;
using System.Collections.Generic;
using System.Linq;
using HotelApi.DbManager;

namespace HotelApi.Repository
{
    public class HotelRegionRepository : IRepository<HotelRegion>
    {
        public HotelRegion Add(HotelRegion obj)
        {
            if (obj.Id == null)
            {
                HotelRegion temp = new HotelRegion(obj);

                using (var context = HotelContextFactory.SingleInstance)
                {
                    context.HotelRegions.Add(temp);
                    context.SaveChanges();
                }

                return temp;
            }
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

            using (var context = HotelContextFactory.SingleInstance)
            {
                context.SaveChanges();
            }
        }

        public HotelRegion Remove(HotelRegion obj)
        {
            var temp = Get(obj);

            using (var context = HotelContextFactory.SingleInstance)
            {
                context.HotelRegions.Remove(temp);
                context.SaveChanges();
            }

            return temp;
        }

        public List<HotelRegion> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<HotelRegion> Search(HotelRegion obj)
        {
            throw new System.NotImplementedException();
        }

        private HotelRegion SearchSingle(Func<HotelRegion, bool> method)
        {
            HotelRegion region;

            using (var context = HotelContextFactory.SingleInstance)
            {
                region = context.HotelRegions.Single(method);
            }

            return region;
        }
    }
}