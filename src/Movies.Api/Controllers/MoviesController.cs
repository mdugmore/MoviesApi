using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Contract.Request;
using Movies.Core.Services;
using Movies.Domain.Common;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        ///     Search movies
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns>List of Movie Results</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMovies([FromQuery] MovieSearchRequest searchRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movies = _movieService.SearchMovies(searchRequest);

            if (movies?.Any() != true) return NotFound("No movies found for search criteria");

            return Ok(movies);
        }

        /// <summary>
        ///     Get top rated movies
        /// </summary>
        /// <param name="count">amount of movies to return. Defaults to 5</param>
        /// <returns>A sorted list of Top Rated Movies</returns>
        [HttpGet("TopRated/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTopRatedMovie(int count = 5)
        {
            if (count < 0) return BadRequest("Positive count required");

            var topMovies = _movieService.GetTopRatedMovies(count);

            if (topMovies?.Any() != true) return NotFound("No movies found in database");

            return Ok(topMovies);
        }

        /// <summary>
        ///     Get top rated movies for a specified user
        /// </summary>
        /// <param name="userId">userId to search user ratings</param>
        /// <param name="count">amount of movies to return. Defaults to 5</param>
        /// <returns>A sorted list of Top Rated Movies rated by the specified user</returns>
        [HttpGet("TopRated/{userId}/{count}")]
        public IActionResult GetTopRatedUserMovie(int userId, int count = 5)
        {
            var userTopMovies = _movieService.GetTopRatedUserMovies(userId, count);
            if (userTopMovies?.Any() != true) return NotFound("No movies found in database");

            return Ok(userTopMovies);
        }

        /// <summary>
        ///     Allow user to Rate a movie. If already rating, rating will be updated
        /// </summary>
        /// <param name="userRatingUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("Rating")]
        public async Task<IActionResult> PutMovie(UserRatingUpdateRequest userRatingUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updateRatingResponse = await _movieService.RateMovie(userRatingUpdateRequest);

            if (updateRatingResponse.RatingUpdateStatus == RatingUpdateStatus.NotFound)
                return NotFound($"Could not find Movie with Id: {userRatingUpdateRequest.MovieId} to update rating");

            return StatusCode(updateRatingResponse.RatingUpdateStatus == RatingUpdateStatus.Added
                ? StatusCodes.Status201Created
                : StatusCodes.Status204NoContent);
        }
    }
}