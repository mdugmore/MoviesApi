using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Contract.Request;
using Movies.Domain.Contract.Response;

namespace Movies.Core.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieResponse> SearchMovies(MovieSearchRequest movieSearchRequest);
        IEnumerable<MovieResponse> GetTopRatedMovies(int count);
        IEnumerable<MovieResponse> GetTopRatedUserMovies(int userId, int count = 5);
        Task<UpdateRatingResponse> RateMovie(UserRatingUpdateRequest userRatingUpdateRequest);
    }
}