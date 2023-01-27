using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if(celestialObject is null) 
                return NotFound();
            return Ok(celestialObject);
        }
        [HttpGet("name")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(x => x.Name == name);
            if (celestialObject is null)
                return NotFound();
            return Ok(celestialObject);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
           return Ok(_context.CelestialObjects);

        }
    }
}
