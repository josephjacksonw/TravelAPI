using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelContext _db;

    public ReviewsController(TravelContext db)
    {
      _db = db;
    }

    // GET api/reviews/
    [HttpGet]
    public async Task<List<Review>> Get(int userRating)
    {
      IQueryable<Review> query = _db.Reviews.AsQueryable();

      if (userRating > 0)
      {
        query = query.Where(entry => entry.UserRating == userRating);
      }

      return await query.ToListAsync();
    }
    // GET api/reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _db.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // POST api/reviews
    [HttpPost]
    public async Task<ActionResult<Review>> Post(Review review)
    {
      _db.Reviews.Add(review);
      await _db.SaveChangesAsync();

      // return CreatedAtAction("Post", new { id = review.ReviewId }, review);
      return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }

    // PUT: api/reviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Review review)
    {
      if (id != review.ReviewId)
      {
        return BadRequest();
      }

      _db.Entry(review).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReviewExists(id))
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

    // DELETE: api/reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }

      _db.Reviews.Remove(review);
      await _db.SaveChangesAsync();

      return NoContent();
    }
    
    private bool ReviewExists(int id)
    {
      return _db.Reviews.Any(e => e.ReviewId == id);
    }
  }
}