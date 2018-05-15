using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain
{
    public class Hotel
    {
        [Key] public int? Id { get; set; }

        [Required] public string Name { get; set; }

        public int? RoomsAvailable { get; set; }

        [Required]
        public int? HotelRegionId { get; set; }

        [Required]
        public HotelRegion Region { get; set; }

        public Hotel()
        {
            RoomsAvailable = 0;
        }
    }
}