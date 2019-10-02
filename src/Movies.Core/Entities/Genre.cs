using System.Collections.Generic;

namespace Movies.Core.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
    }
}