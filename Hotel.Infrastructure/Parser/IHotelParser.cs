using System.Collections.Generic;

namespace Hotel.Infrastructure.Parser
{
    public interface IHotelParser
    {
        List<Domain.Hotel> Parse(params string[] value);
    }
}
