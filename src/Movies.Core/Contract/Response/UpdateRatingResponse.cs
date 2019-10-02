using Movies.Domain.Common;

namespace Movies.Domain.Contract.Response
{
    public class UpdateRatingResponse
    {
        public int MovieId { get; set; }
        public RatingUpdateStatus RatingUpdateStatus { get; set; }
    }
}