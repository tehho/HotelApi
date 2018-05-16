using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure.DbManager;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Controllers
{
    [Route("[controller]")]
    public class TestsController : Controller
    {

        private readonly IRepository<HotelRegion> _hotelRepository;
        private readonly AppConfiguration _appConfiguration;
        private readonly HotelContext _context;



        public TestsController(IRepository<HotelRegion> hotelsRepository, AppConfiguration appConfiguration,
            HotelContext context)
        {
            _hotelRepository = hotelsRepository;
            _appConfiguration = appConfiguration;
            _context = context;
        }

        [HttpGet("Database")]
        public IActionResult GetAllHotels()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
            }
            catch (Exception e)
            {
                return StatusCode(503);
            }
            return Ok("Database works");
        }


    }
}
