using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Contract.Request
{
    public class UserRatingUpdateRequest
    {
        [Required] public int MovieId { get; set; }

        [Required] public int UserId { get; set; }

        [Required] [Range(0, 5)] public int Rating { get; set; }
    }
}