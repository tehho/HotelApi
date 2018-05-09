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

        [HttpGet, Route("DisplayAllRegions")]
        public IActionResult DisplayAllRegions()
        {
            var regions = _hotelsRepository.Get();
            return Ok(regions);
        }

        [HttpPost, Route("Add")]
        public IActionResult AddRegion()
        {
            _hotelsRepository.Add();
            return Ok(message);
        }



        [HttpDelete("DeleteRegion")]
        public IActionResult Remove(int id)
        {
            _hotelsRepository.Remove(id);
            return Ok("Region Deleted");
        }

        [HttpDelete("RecedDatabase")]
        public IActionResult RecedDatabase()
        {
            _hotelsRepository.Reced();
            return Ok("Region Deleted");
        }
    }
}
