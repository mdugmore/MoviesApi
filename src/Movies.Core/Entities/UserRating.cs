namespace Movies.Core.Entities
{
    public class UserRating
    {
        public int UserRatingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public int MovieId { get; set; }
    }
}