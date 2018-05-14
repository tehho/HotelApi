using System;
using System.Collections.Generic;
using Hotel.Domain;

namespace Hotel.App
{
    public class Program
    {
        static void Main(string[] args)
        {

           

            var hotels= new List<Domain.Hotel>();
            hotels.Add(new Domain.Hotel
            { 
               Name  = "Scandic Rubinen",
                HotelRegionId = 50,
            });
            hotels.Add(new Domain.Hotel
            {
                Name = "Scandic Opalen",
                HotelRegionId = 50,
            });
            hotels.Add(new Domain.Hotel
            {
                Name = "Scandic Backadal",
                HotelRegionId = 60,
            });
            hotels.Add(new Domain.Hotel
            {
                Name = "Scandic Helsingborg North",
                HotelRegionId = 70,
            });
            var scandisRoomGenerator= new FreeRoomGenerator("../../../Scandic");

            var hotelLista=scandisRoomGenerator.GetListOfFreeRooms(hotels);
            scandisRoomGenerator.CreateFreeRoomFile(hotelLista);

        }
    }
}
