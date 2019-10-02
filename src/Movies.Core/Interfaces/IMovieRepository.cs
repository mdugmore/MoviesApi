using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovie(int movieId);
        Task<UserRating> GetUserRating(int movieId, int userId);
        IEnumerable<Movie> GetMovies();
        Task<int> AddMovieRating(int movieId, int userId, int rating);
        Task<int> UpdateMovieRating(UserRating userRating);
    }
}