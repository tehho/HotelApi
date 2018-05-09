using System.ComponentModel.DataAnnotations;

namespace HotelApi
{
    public class HotelRegion
    {

        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "A region needs a name")]
        public string Name { get; set; }

        public HotelRegion()
        {

        }

        public HotelRegion(HotelRegion other)
        {
            Id = other.Id;
            Name = other.Name;
        }
        
        public HotelRegion(string name)
        {
            Name = name;
            Id = null;
        }
        public HotelRegion(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }

   

}