namespace Hotel.Infrastructure.Parser
{
    public interface IHotelSerializer
    {
        string Serializer(Domain.Hotel hotel);
    }

    public class ScandicSerializer : IHotelSerializer
    {
        public string Serializer(Domain.Hotel hotel)
        {
            return $"{hotel.HotelRegionId},{hotel.Name},{hotel.RoomsAvailable}";
        }
    }
}