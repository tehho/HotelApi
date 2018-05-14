namespace Hotel.Domain
{
    public class HotelRegionSearcher
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public HotelRegion ToHotelRegion()
        {
            return new HotelRegion()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
