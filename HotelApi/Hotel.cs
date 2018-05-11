using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApi
{
    public class Hotel
    {
        [Key] public int? Id { get; set; }

        [Required] public string Name { get; set; }

        public int RoomsAvaiable { get; set; }

        [Required]
        public int HotelRegionId { get; set; }

        [Required]
        public HotelRegion Region { get; set; }

        public Hotel()
        {
            RoomsAvaiable = 0;
        }
    }
}