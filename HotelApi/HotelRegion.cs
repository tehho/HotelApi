using System.ComponentModel.DataAnnotations;

namespace HotelApi
{
    public class HotelRegion
    {
        public HotelRegion(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key] public int? Id { get; set; }

        [Required] public string Name { get; set; }

        public HotelRegion(HotelRegion other)
        {
            Id = other.Id;
            Name = other.Name;
        }

        public HotelRegion()
        { }

    }

   

}