namespace Hotel.Infrastructure.Parser
{
    public interface IHotelParser
    {
        Hotel.Domain.Hotel Parse(params string[] value);
    }
}
