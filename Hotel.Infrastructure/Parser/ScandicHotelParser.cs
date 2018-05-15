using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Infrastructure.Parser
{
    public class ScandicHotelParser : IHotelParser
    {
        public List<Domain.Hotel> Parse(params string[] value)
        {
            return value.Select(line =>
            {
                var list = line.Split(',');

                //TODO Magic strings?
                if (list.Length != 3)
                    throw new FormatException("Value format is wrong!");
                
                if (!int.TryParse(list[0], out var regionId))
                    throw new ArgumentException("Region Id is not a number");

                if (!int.TryParse(list[2], out var numberOfRooms))
                    throw  new ArgumentException("Number of free rooms is not a number");

                return new Domain.Hotel()
                {
                    HotelRegionId = regionId,
                    Name = list[1],
                    RoomsAvailable = numberOfRooms
                };
            }).ToList();
        }
    }
}