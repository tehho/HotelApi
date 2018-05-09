using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.DbManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelApi.Controllers
{
    [Route("api/hotels")]
    public class HotelsController : Controller
    {
        private readonly IRepository<HotelRegion> _hotelsRepository;

        public HotelsController(IRepository<HotelRegion> hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        [HttpGet, Route("DisplayAllRegions")]
        public IActionResult DisplayAllRegions()
        {
            var regions = _hotelsRepository.GetAll();
            return Ok(regions);
        }

        [HttpPost, Route("Add")]
        public IActionResult AddRegion(HotelRegion region)
        {
            var returnRegion = _hotelsRepository.Add(region);
            return Ok(returnRegion);
        }

        [HttpDelete("DeleteRegion")]
        public IActionResult Remove(HotelRegion region)
        {
            HotelRegion temp = null;
            try
            {
                temp = _hotelsRepository.Remove(region);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
            }
            return Ok(temp);
        }

        [HttpDelete("ReseedDatabase")]
        public IActionResult ReseedDatabase()
        {
            if (_hotelsRepository.Reseed())
            {
                AddRegion(new HotelRegion() { Name = "Göteborg Centrum", Id = 50 });
                AddRegion(new HotelRegion() { Name = "Göteborg Hisingen", Id = 60 });
                AddRegion(new HotelRegion() { Name = "Helsingborg", Id = 70 });
                return Ok("Database reseeded");
            }
            return Ok("Reseeding failed");
        }
    }
}
