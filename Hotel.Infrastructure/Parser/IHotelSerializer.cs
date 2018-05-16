namespace Hotel.Infrastructure.Parser
{
    public interface IHotelSerializer
    {
        string Serializer(Domain.Hotel hotel);
    }
}