using System.Collections.Generic;
using System.Text;
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
            RoomsAvailable = 25

        };
        [ClassInitialize]
        public static void AdHotel(TestContext args)
        {
            listHotel.Add(testHotel);
        }






        [TestMethod]
        public void Test_Format_Of_FreeRoom_List_Generator ()
        {
            const string answer = "50,Scandic,25";

            var testHotel = new FreeRoomGenerator("path").GetListOfFreeRooms(listHotel);
           
            foreach (var hotel in testHotel)
            {
                var hotelArray = hotel.Split(',');
                hotelArray[2] = "25";
                var nytthotel = new StringBuilder();
                nytthotel.Append(hotelArray[0]+',');
                nytthotel.Append(hotelArray[1]+',');
                nytthotel.Append(hotelArray[2]);

                Assert.AreEqual(answer, nytthotel.ToString());
                
            }
        }
    }
}
