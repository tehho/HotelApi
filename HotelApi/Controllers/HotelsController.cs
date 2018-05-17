using System;
using System.Collections.Generic;
using System.IO;
using Hotel.Infrastructure.Parser;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IRepository<Hotel.Domain.Hotel> _hotelRepository;
        private readonly AppConfiguration _appConfiguration;

        public HotelsController(IRepository<Hotel.Domain.Hotel> hotelRepository, AppConfiguration appConfiguration)
        {
            _hotelRepository = hotelRepository;
            _appConfiguration = appConfiguration;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            return Ok(_hotelRepository.GetAll());
        }

        [HttpPost]
        public IActionResult AddHotel([FromBody]Hotel.Domain.Hotel hotel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                if (hotel.Name.ToLower().Contains("scandic"))
                {
                    AddToFileScandic(hotel);
                }
                else if (hotel.Name.ToLower().Contains("bestwestern"))
                {
                    AddToFileBestwestern(hotel);
                }
                else
                {
                    BadRequest($"Hotelname not recognized: {hotel.Name}");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Hotel added to todays list");
        }

        private void AddToFileBestwestern(Hotel.Domain.Hotel hotel)
        {
            var path = _appConfiguration.ScandicHotels + $"/Bestwestern-{DateTime.Now:yyyy-MM-dd}.json";
            List<Hotel.Domain.Hotel> list;
            try
            {
                list = JArray.Parse(System.IO.File.ReadAllText(path)).ToObject<List<Hotel.Domain.Hotel>>();
            }
            catch (Exception e)
            {
                list = new List<Hotel.Domain.Hotel>();
            }
            list.Add(hotel);

            using (var writer = System.IO.File.CreateText(path))
            {
                writer.Write(JsonConvert.SerializeObject(list));
            }

        }

        private void AddToFileScandic(Hotel.Domain.Hotel hotel)
        {
            var path = _appConfiguration.ScandicHotels + $"/Scandic-{DateTime.Now:yyyy-MM-dd}.txt";

            using (var writer = new StreamWriter(path, false))
                writer.WriteLine(new ScandicSerializer().Serializer(hotel));
        }

        [HttpDelete("Reseed")]
        public IActionResult ReseedHotels()
        {
            if (_hotelRepository.Reseed())
            {
                return Ok("Reseed successfull");
            }

            return BadRequest("Reseed failed. Contact system administrator");
        }
    }
}
