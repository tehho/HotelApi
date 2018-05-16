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
        public IActionResult AddHotel(Hotel.Domain.Hotel hotel)
        {
            if (hotel == null)
                return BadRequest();
            try
            {
                if (hotel.Name.Contains("Scandic"))
                {
                    AddToFileScandic(hotel);
                }
            }
            catch (ArgumentException)
            {
                _hotelRepository.Add(hotel);
            }

            return Ok();
        }

        private void AddToFileScandic(Hotel.Domain.Hotel hotel)
        {
            var path = _appConfiguration.ScandicHotels + $"/Scandic-{DateTime.Now:yyyy-MM-dd}.txt";
            
            System.IO.File.AppendText(path).WriteLine(new ScandicSerializer().Serializer(hotel));
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
