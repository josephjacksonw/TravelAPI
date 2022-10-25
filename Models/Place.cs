using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Travel.Models
{
  public class Place
  {
    public Place()
    {
      this.Reviews = new HashSet<Review>();
    }
    public int PlaceId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between a 1 and 5.")]
    public int Rating { get; set; }
    public ICollection<Review> Reviews { get; set; }
  }
}