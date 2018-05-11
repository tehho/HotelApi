﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApi.Parser
{
    public interface IHotelParser
    {
        Hotel Parse(params string[] value);
    }

    public class ScandicHotelParser : IHotelParser
    {
        public Hotel Parse(params string[] value)
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

                return new Hotel()
                {
                    Id = int.Parse(list[0]),
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