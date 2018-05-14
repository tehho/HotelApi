using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HotelApi.DbManager;
using HotelApi.Parser;
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

            regions = FillRegionsWithHotels(regions);

            return Ok(regions);
        }

        private List<HotelRegion> FillRegionsWithHotels(List<HotelRegion> regions)
        {
            string loadFile = GetLastScandicFreeRooms();

            if (loadFile != "")
            {
                var list = System.IO.File.ReadAllLines(loadFile).ToList();

                regions.ForEach(r => r.Hotels = new List<Hotel>());

                list.ForEach(line =>
                {
                    try
                    {
                        var hotel = new ScandicHotelParser().Parse(line);
                        var region = regions.Single(r => r.Id == hotel.HotelRegionId);

                        region.Hotels.Add(hotel);
                    }
                    catch (InvalidOperationException ioe)
                    {
                        //TODO Logga att regionen inte fanns
                    }
                    catch (ArgumentException e)
                    {
                        //TODO Logga att det var fel att läsa Scandic filen
                    }


                });
            }

            return regions;
        }

        private static string GetLastScandicFreeRooms()
        {
            var files = Directory.GetFiles("Scandic/").ToList();
            var regex = new Regex(@"Scandic-^(\d{4})-(\d{2})-(0[1-9]|[1-2]{1}[0-9]{1}|30|31)\.txt$");

            DateTime now = DateTime.MinValue;
            var loadFile = "";

            foreach (var file in files)
            {
                if (regex.IsMatch(file))
                {
                    var matches = regex.Match(file);
                    var year = int.Parse(matches.Groups[1].Value);
                    var month = int.Parse(matches.Groups[2].Value);
                    var day = int.Parse(matches.Groups[3].Value);

                     var test = new DateTime(year, month, day);
                     

                    if (test > now)
                    {
                        now = test;
                        loadFile = file;
                    }
                }
            }

            return loadFile;
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
