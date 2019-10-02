using System;
using System.Collections.Generic;

namespace Movies.Core.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
        public IEnumerable<UserRating> UserRatings { get; set; }
    }
}