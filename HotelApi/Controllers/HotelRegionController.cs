using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure.Parser;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    public class HotelRegionController : Controller
    {
        private readonly IRepository<HotelRegion> _hotelsRepository;
        private readonly AppConfiguration _appConfiguration;
        private readonly IHotelParser _scandicParser;
        private readonly IHotelParser _bestWesternParser;

        public HotelRegionController(IRepository<HotelRegion> hotelsRepository, AppConfiguration appConfiguration)
        {
            _hotelsRepository = hotelsRepository;
            _appConfiguration = appConfiguration;
            _scandicParser = new ScandicHotelParser();
            _bestWesternParser = new BestWesternHotelParser();
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
            regions.ForEach(region => region.Hotels = new List<Hotel.Domain.Hotel>());

            regions = FillRegionsWithScandicHotels(regions);

            regions = FillRegionsWithBestWesternHotels(regions);

            return regions;
        }

        private List<HotelRegion> FillRegionsWithBestWesternHotels(List<HotelRegion> regions)
        {
            var regex = new Regex($"BestWestern-(20\\d\\d)-(0[1-9]|10|11|12)-(0[1-9]|[1-2][0-9]|30|31)\\.json");

            var loadFile = Directory.GetFiles(_appConfiguration.ScandicHotels)
                .Where(file => regex.IsMatch(file))
                .OrderBy(System.IO.File.GetCreationTime).First();

            if (loadFile == null)
            {
                throw new InvalidOperationException();
            }

            _bestWesternParser.Parse(System.IO.File.ReadAllLines(loadFile))
                .ForEach(hotel =>
                {
                    var region = regions.Single(r => r.Id == hotel.HotelRegionId);
                    region.Hotels.Add(hotel);
                });

            return regions;
        }

        private List<HotelRegion> FillRegionsWithScandicHotels(List<HotelRegion> regions)
        {
            var regex = new Regex($"Scandic-(20\\d\\d)-(0[1-9]|10|11|12)-(0[1-9]|[1-2][0-9]|30|31)\\.txt$");
            
            var loadFile = Directory.GetFiles(_appConfiguration.ScandicHotels)
                .Where(file => regex.IsMatch(file))
                .OrderBy(System.IO.File.GetCreationTime).First();

            if (loadFile == null)
            {
                throw new InvalidOperationException();
            }

            _scandicParser.Parse(System.IO.File.ReadAllLines(loadFile))
                .ForEach(hotel =>
            {
                var region = regions.Single(r => r.Id == hotel.HotelRegionId);
                region.Hotels.Add(hotel);
            });

            return regions;
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
