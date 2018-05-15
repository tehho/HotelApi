using System;
using System.Linq;

namespace Hotel.Infrastructure.Parser
{
    public class ScandicHotelParser : IHotelParser
    {
        public Hotel.Domain.Hotel Parse(params string[] value)
        {
            if (value.Length == 1)
            {
                var list = value[0].Split(",");
                if (list.Length == 1)
                    throw new ArgumentException("Hotel", "Value in ScandicHotelParser was not compattable with Scandic parser code.");

                return Parse(list);
            }


            try
            {
                var list = value.Select(s => s.Trim()).ToList();

                return new Hotel.Domain.Hotel()
                {
                    HotelRegionId = int.Parse(list[0]),
                    Name = list[1],
                    RoomsAvaiable = int.Parse(list[2])
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException("Hotel", "Value in ScandicHotelParser was not compattable with Scandic parser code.");
            }
        }
    }
}