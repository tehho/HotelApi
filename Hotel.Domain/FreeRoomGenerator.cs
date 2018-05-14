using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Hotel.Domain
{
    public class FreeRoomGenerator
    {
        private string _path;

        public FreeRoomGenerator(string path)
        {
            _path = path;
        }
        public List<string> GetListOfFreeRooms(List<Hotel> hotels)
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
        public void CreateFreeRoomFileJson(List<Hotel> hotels)
        {
            var randomRoom = new Random();
            List<Hotel> _data = new List<Hotel>();

            foreach (var hotel in hotels)
            {
                var randomFreeRoom = randomRoom.Next(0, 25);
                _data.Add(new Hotel()
                {
                    HotelRegionId = hotel.HotelRegionId,
                    Name = hotel.Name,
                    RoomsAvaiable = randomFreeRoom
                });
            }
            

            string json = JsonConvert.SerializeObject(_data.ToArray());

           
            System.IO.File.WriteAllText($"{_path}/BestWestern-{DateTime.Now:yyyy-MM-dd}.json", json);


        }

    }


}
