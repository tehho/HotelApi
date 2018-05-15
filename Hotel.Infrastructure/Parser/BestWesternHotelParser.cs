using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Hotel.Infrastructure.Parser
{
    public class BestWesternHotelParser : IHotelParser
    {
        public List<Domain.Hotel> Parse(params string[] value)
        {
            return JArray.FromObject(value).ToObject<List<Domain.Hotel>>();
        }
    }
}
