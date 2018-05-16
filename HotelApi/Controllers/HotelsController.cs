using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Infrastructure.Parser;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult AddHotel([FromBody]Hotel.Domain.Hotel hotel)>
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (hotel.Name.Contains("Scandic"))
            {
                AddToFileScandic(hotel);
            }
            else if (hotel.Name.Contains("Bestwestern"))
            {
                AddToFileBestwestern(hotel);
            }
            else
            {
                BadRequest($"Hotelname not recognized: {hotel.Name}");
            }

            return Ok("Hotel added to todays list");
        }

        private void AddToFileBestwestern(Hotel.Domain.Hotel hotel)
        {

        }

        private void AddToFileScandic(Hotel.Domain.Hotel hotel)
        {
            var path = _appConfiguration.ScandicHotels + $"/Scandic-{DateTime.Now:yyyy-MM-dd}.txt";

            using (var writer = System.IO.File.AppendText(path))
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
