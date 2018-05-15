using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotel.Domain.Test
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly List<Hotel> listHotel = new List<Hotel>();
        
        private static readonly Hotel testHotel = new Hotel
        {
            HotelRegionId = 50,
            Name = "Scandic",
            RoomsAvaiable = 25

        };
        [ClassInitialize]
        public static void AdHotel(TestContext args)
        {
            listHotel.Add(testHotel);
        }






        [TestMethod]
        public void TestMethod1()
        {
            const string answer = "50,Scandic,25";

            var testHotel = new FreeRoomGenerator("path").GetListOfFreeRooms(listHotel);
           
            foreach (var hotel in testHotel)
            {
               
                Assert.AreEqual(answer, hotel);
            }
        }
    }
}
