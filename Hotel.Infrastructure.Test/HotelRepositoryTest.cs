using System;
using System.Collections.Generic;
using System.Text;
using Hotel.Domain;
using Hotel.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotel.Infrastructure.Test
{
    [TestClass]
    public class HotelRepositoryTest
    {
        private static IRepository<Domain.Hotel> _repo;

        [ClassInitialize]
        public static void Initzilise(TestContext args)
        {
            _repo = new HotelRepository(new MockDbManager());
        }

        [TestMethod]
        public void Test_Add_Name_Null_Expect_Exception()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _repo.Add(new Domain.Hotel() { Name = null });

            });
        }

        [TestMethod]
        public void Test_Remove_Hotel_Expect_Removed()
        {
           var hotel = new Domain.Hotel()
           {
               Id= 1,
           };
            _repo.Add(hotel);
            Assert.Equals(1, _repo.GetAll().Count);
            _repo.Remove(hotel);
            Assert.Equals(0, _repo.GetAll().Count);
        }
    }

}
