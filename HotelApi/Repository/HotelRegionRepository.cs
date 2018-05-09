using System;
using System.Collections.Generic;
using HotelApi.DbManager;

namespace HotelApi.Repository
{
    public class HotelRegionRepository : IRepository<HotelRegion>
    {
        public HotelRegion Add(HotelRegion obj)
        {
            HotelRegion temp = new HotelRegion(obj);
            temp.Id = null;

            using (var context = HotelContextFactory.SingleInstance)
            {
                context.HotelRegions.Add(temp);
                context.SaveChanges();
            }

            return temp;
        }

        public HotelRegion Get(HotelRegion obj)
        {
            throw new System.NotImplementedException();
        }

        public HotelRegion Update(HotelRegion obj)
        {
            throw new System.NotImplementedException();
        }

        public HotelRegion Remove(HotelRegion obj)
        {
            throw new System.NotImplementedException();
        }

        public List<HotelRegion> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<HotelRegion> Search(HotelRegion obj)
        {
            throw new System.NotImplementedException();
        }
    }
}