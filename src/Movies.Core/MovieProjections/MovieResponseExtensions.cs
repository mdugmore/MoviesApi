using System.Linq;
using Movies.Core.Entities;
using Movies.Domain.Contract.Response;

namespace Movies.Core.MovieProjections
{
    public static class MovieResponseExtensions
    {
        public static MovieResponse ToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.MovieId,
                AverageRating = movie.UserRatings != null && movie.UserRatings.Any()
                    ? movie.UserRatings.Average(r => r.Rating)
                    : (double?) null,
                RunningTime = movie.RunningTimeInMinutes,
                Title = movie.Title,
                YearOfRelease = movie.ReleaseDate.Year
            };
        }
    }
}