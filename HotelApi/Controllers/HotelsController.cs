using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Hotel.Domain;
using HotelApi.DbManager;
using HotelApi.Parser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hotel = Hotel.Domain.Hotel;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IRepository<HotelRegion> _hotelsRepository;
        private readonly AppConfiguration _appConfiguration;

        public HotelsController(IRepository<HotelRegion> hotelsRepository, AppConfiguration appConfiguration)
        {
            _hotelsRepository = hotelsRepository;
            _appConfiguration = appConfiguration;
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
            regions.ForEach(r => r.Hotels = new List<global::Hotel.Domain.Hotel>());

            regions = FillRegionsWithScandicHotels(regions);

            regions = FillRegionsWithBestWesternHotels(regions);

            return regions;
        }

        private List<HotelRegion> FillRegionsWithBestWesternHotels(List<HotelRegion> regions)
        {
            try
            {
                var loadFile = GetLastBestWesternFreeRooms();

                if (loadFile != "")
                {

                    var fileContent = System.IO.File.ReadAllText(loadFile);

                    var list = JArray.Parse(fileContent).ToObject<List<global::Hotel.Domain.Hotel>>();

                    foreach (var hotel in list)
                    {

                    }

                    list.ForEach(hotel =>
                    {
                        try
                        {
                            var region = regions.Single(r => r.Id == hotel.HotelRegionId);

                            region.Hotels.Add(hotel);
                        }
                        catch (InvalidOperationException ioe)
                        {
                            //TODO Logga att regionen inte fanns
                        }
                    });
                }
            }
            catch (Exception e)
            {
                //TODO Logga att det var fel att läsa Scandic filen
            }

            return regions;
        }

        private List<HotelRegion> FillRegionsWithScandicHotels(List<HotelRegion> regions)
        {
            try
            {
                var loadFile = GetLastScandicFreeRooms();

                if (loadFile != "")
                {
                    var list = System.IO.File.ReadAllLines(loadFile).ToList();


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
            }
            catch (Exception e)
            {

            }

            return regions;
        }

        private string GetLastScandicFreeRooms()
        {
            var files = Directory.GetFiles(_appConfiguration.ScandicHotels).ToList();
            var regex = new Regex(@"Scandic-(2[0-1][0-9]{2})-(0[1-9]|[1]{1}[1-2]{1})-(0[1-9]|[1-2]{1}[0-9]{1}|30|31)\.txt$");

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
        private string GetLastBestWesternFreeRooms()
        {
            var files = Directory.GetFiles(_appConfiguration.ScandicHotels).ToList();
            var regex = new Regex(@"BestWestern-(2[0-1][0-9]{2})-(0[1-9]|[1]{1}[1-2]{1})-(0[1-9]|[1-2]{1}[0-9]{1}|30|31)\.json$");

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
