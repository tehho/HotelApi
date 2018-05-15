using System;
using Hotel.Domain;
using Hotel.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotel.Infrastructure.Test
{
    [TestClass]
    public class HotelregionRepositoryTest
    {
        private static IRepository<HotelRegion> _repo;

        [ClassInitialize]
        public static void Initzilise(TestContext args)
        {
            _repo= new HotelRegionRepository(new MockDbManager());
        }

        [TestMethod]
        public void Test_Update_ID_Null_Expect_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _repo.Update(new HotelRegion() {Id = null}); 

            });
        }
        [TestMethod]
        public void Test_Get_ID_Not_Exists_Expect_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _repo.Get(new HotelRegion() { Id = 1 });

            });
        }
    }
}