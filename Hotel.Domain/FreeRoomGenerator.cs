using System;
using System.Collections.Generic;
using System.IO;

namespace Hotel.Domain
{
    public class FreeRoomGenerator
    {
        private string _path;

        public FreeRoomGenerator(string path)
        {
            _path = path;
        }
        public  List<string> GetListOfFreeRooms(List<Hotel> hotels)
        {
            var randomRoom = new Random();
            var listaHotel = new List<string>();

            foreach (var hotel in hotels)
            {
                var randomFreeRoom = randomRoom.Next(0, 25);
                var text = $"{hotel.HotelRegionId},{hotel.Name},{randomFreeRoom}";
                listaHotel.Add(text);
            }
            return listaHotel;
        }
        public void CreateFreeRoomFile(List<string> listaHotel)
        {
            using (var writer = new StreamWriter($"{_path}/Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                foreach (var hotel in listaHotel)
                {
                    writer.WriteLine(hotel);
                }

            }
        }

    }
    
   
}
