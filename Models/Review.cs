using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Travel.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between a 1 and 5.")]
    public int UserRating { get; set; }
    public int PlaceId { get; set; }
    [JsonIgnore]
    public virtual Place Place { get; set; }
  }
}