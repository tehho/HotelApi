using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.DbManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IRepository<HotelRegion> _hotelsRepository;

        public HotelsController(IRepository<HotelRegion> hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        [HttpGet]
        public IActionResult DisplayAllRegions()
        {
            var regions = _hotelsRepository.GetAll();
            return Ok(regions);
        }

        [HttpGet("{Id}")]
        public IActionResult DisplayRegion(HotelRegionSearcher region)
        {
            if (region.Id == null)
                return BadRequest("No ID Given for DisplayRegion");

            HotelRegion returnRegion;
            try
            {
                returnRegion = _hotelsRepository.Get(region.ToHotelRegion());

            }
            catch (InvalidOperationException ioe)
            {
                return NotFound(ioe.Message);
            }

            return Ok(returnRegion);
        }
        
        [HttpPost]
        public IActionResult AddRegion(HotelRegion region)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnRegion = _hotelsRepository.Add(region);
            return Ok(returnRegion);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(HotelRegionSearcher region)
        {
            if (region.Id == null)
                return BadRequest(ModelState);

            HotelRegion temp = null;
            try
            {
                temp = _hotelsRepository.Remove(region.ToHotelRegion());
            }
            catch (InvalidOperationException ioe)
            {
                return NotFound(ioe.Message);
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
            return BadRequest("Reseeding failed");
        }
    }
}
