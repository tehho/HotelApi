using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelApi.Controllers
{
    [Route("api/hotels")]
    public class HotelsController : Controller
    {
        private readonly IHotelsRepository _hotelsRepository;

        public HotelsController(IHotelsRepository hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpGet, Route("DisplayAllHotels")]
        public IActionResult DisplayAllHotels()
        {
            var hotels = _hotelsRepository.GetAllHotels();
            return Ok(hotels);
        }

        
        [HttpGet, Route("DisplayAllRegions")]
        public IActionResult DisplayAllRegions()
        {
            var regions = _hotelsRepository.GetAllRegions();
            return Ok(regions);
        }

        [HttpPost, Route("AddRegion")]
        public IActionResult AddRegion()
        {
            _hotelsRepository.AddRegions();
            return Ok(message);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOneRegion(int id)
        {
            _hotelsRepository.DeleteRegion(id);
            return Ok("Region Deleted");
        }
    }
}
