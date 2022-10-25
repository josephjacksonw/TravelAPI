using System;
using System.Collections.Generic;
using System.Linq; 
// using System.Data.Entity;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlacesController : ControllerBase
  {
    private readonly TravelContext _db;

    public PlacesController(TravelContext db)
    {
      _db = db;
    }

    // GET api/places/
    [HttpGet]
    public async Task<List<Place>> Get(string country, string name, int rating)
    {
      IQueryable<Place> query = _db.Places.Include(entry => entry.Reviews).AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name).Include(entry => entry.Reviews);
      }

      if (country != null)
      {
        query = query.Where(entry => entry.Country == country).Include(entry => entry.Reviews);
      }

      if (rating > 0)
      {
        query = query.Where(entry => entry.Rating == rating).Include(entry => entry.Reviews);
      }

      return await query.ToListAsync();
    }

    [HttpGet("random")]
    public async Task<ActionResult<IEnumerable<Place>>> GetRandom()
    {
      int lower = 1;
      int upper = _db.Places.Count() + 1;
      Random rnd = new Random();
      int id = rnd.Next(lower, upper);
      IQueryable<Place> quer = _db.Places.Include(entry => entry.Reviews).AsQueryable();
      quer = quer.Where(entry => entry.PlaceId == id).Include(entry => entry.Reviews);
      return await quer.ToListAsync();
    }
  
    // GET api/places/5
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Place>>> GetPlace(int id)
    {
      IQueryable<Place> place = _db.Places.Include(entry => entry.Reviews).AsQueryable();
      place = place.Where(entry => entry.PlaceId == id).Include(entry => entry.Reviews);
      if (id > _db.Places.Count())
      {
        return NotFound();
      }
      return await place.ToListAsync();
    }

    // POST api/places
    [HttpPost]
    public async Task<ActionResult<Place>> Post(Place place)
    {
      _db.Places.Add(place);
      await _db.SaveChangesAsync();

      // return CreatedAtAction("Post", new { id = place.PlaceId }, place);
      return CreatedAtAction(nameof(GetPlace), new { id = place.PlaceId }, place);
    }

    // PUT: api/Places/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Place place)
    {
      if (id != place.PlaceId)
      {
        return BadRequest();
      }

      _db.Entry(place).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PlaceExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    // DELETE: api/Places/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlace(int id)
    {
      var place = await _db.Places.FindAsync(id);
      if (place == null)
      {
        return NotFound();
      }

      _db.Places.Remove(place);
      await _db.SaveChangesAsync();

      return NoContent();
    }
    
    private bool PlaceExists(int id)
    {
      return _db.Places.Any(e => e.PlaceId == id);
    }

    
  }
}