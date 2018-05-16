using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("test/[controller]")]
    public class TestsController :Controller
    {
       
            private readonly IRepository<HotelRegion> _hotelRepository;
            private readonly AppConfiguration _appConfiguration;


            public TestsController(IRepository<HotelRegion> hotelsRepository, AppConfiguration appConfiguration)
            {
                _hotelRepository = hotelsRepository;
                _appConfiguration = appConfiguration;
            }

            [HttpGet]
            public IActionResult GetAllHotels()
            {
                return Ok(_hotelRepository.GetAll());
            }

        
}
