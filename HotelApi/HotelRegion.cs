using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApi
{
    public class HotelRegion
    {

        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "A region needs a name")]
        public string Name { get; set; }

        public List<Hotel> Hotels { get; set; }

        public HotelRegion()
        {

        }

        public HotelRegion(HotelRegion other)
        {
            Id = other.Id;
            Name = other.Name;
            Hotels = other.Hotels;
        }
        
        public HotelRegion(string name)
        {
            Name = name;
            Hotels = new List<Hotel>();
            Id = null;
        }
        public HotelRegion(int id, string name, List<Hotel> hotels)
        {
            Id = id;
            Name = name;
            Hotels = hotels;
        }

    }

   

}