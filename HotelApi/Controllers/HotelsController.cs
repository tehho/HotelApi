using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
