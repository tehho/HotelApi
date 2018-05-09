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
        private readonly IRepository<> _hotelsRepository;

        public HotelsController(IRepository<> hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
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



        [HttpDelete("DeleteRegion")]
        public IActionResult DeleteOneRegion(int id)
        {
            _hotelsRepository.DeleteRegion(id);
            return Ok("Region Deleted");
        }

        [HttpDelete("RecedDatabase")]
        public IActionResult RecedDatabase()
        {
            _hotelsRepository.RecedDatabase();
            return Ok("Region Deleted");
        }
    }
}
