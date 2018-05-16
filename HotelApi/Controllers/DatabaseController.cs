using System;
using Hotel.Infrastructure.DbManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Controllers
{
    [Route("Check/[controller]")]
    public class DatabaseController : Controller
    {
        private readonly HotelContext _context;

        public DatabaseController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CheckDatabaseConnectionIsUp()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}