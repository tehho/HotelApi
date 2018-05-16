using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure.DbManager;

namespace Hotel.Infrastructure.Repository
{
    public class TestRepository : IRepository<HotelRegion>
    {
        private readonly HotelContext _context;

        public TestRepository(HotelContext context)
        {
            _context = context;
        }
        public HotelRegion Add(HotelRegion obj)
        {
            throw new NotImplementedException();
        }

        public HotelRegion Get(HotelRegion obj)
        {
            throw new NotImplementedException();
        }

        public HotelRegion Update(HotelRegion obj)
        {
            throw new NotImplementedException();
        }

        public HotelRegion Remove(HotelRegion obj)
        {
            throw new NotImplementedException();
        }

        public List<HotelRegion> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<HotelRegion> Search(HotelRegion obj)
        {
            throw new NotImplementedException();
        }

        public bool Reseed()
        {
            throw new NotImplementedException();
        }
    }
}
