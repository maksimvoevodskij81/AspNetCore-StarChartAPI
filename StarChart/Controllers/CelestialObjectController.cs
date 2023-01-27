﻿using System.Linq;
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
        [HttpGet("{id:int}",Name = "GetById")]
        public IActionResult GetById(int id) 
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if(celestialObject is null) 
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == id).ToList();   
            return Ok(celestialObject);
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.Find(name);
            if (celestialObject is null)
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == celestialObject.Id).ToList();
            return Ok(celestialObject);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.Select(x => x).ToList();
            celestialObjects.ForEach(o => o.Satellites = 
            _context.CelestialObjects.Where(x => x.OrbitedObjectId == o.Id).ToList());

            return Ok(celestialObjects); 

        }
    }
}
