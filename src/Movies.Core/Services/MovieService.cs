using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Core.Contract.Request;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Domain.Common;
using Movies.Domain.Contract.Response;

namespace Movies.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<MovieResponse> SearchMovies(MovieSearchRequest movieSearchRequest)
        {
            return _movieRepository.GetMovies()
                .Where(m => (movieSearchRequest.Genres == null || !movieSearchRequest.Genres.Any() ||
                             m.MovieGenres.Any(g => movieSearchRequest.Genres.Contains(g.Genre.Name)))
                            && (!movieSearchRequest.YearOfRelease.HasValue ||
                                m.ReleaseDate.Year == movieSearchRequest.YearOfRelease)
                            && (string.IsNullOrEmpty(movieSearchRequest.Title) || m.Title.Contains(
                                    movieSearchRequest.Title,
                                    StringComparison.InvariantCultureIgnoreCase)))
                .Select(MapToMovieResponse)
                .ToList();
        }

        public IEnumerable<MovieResponse> GetTopRatedMovies(int count)
        {
            return _movieRepository.GetMovies()
                .Select(MapToMovieResponse)
                .OrderByDescending(m => m.AverageRating)
                .ThenBy(m => m.Title)
                .Take(count)
                .ToList();
        }

        public IEnumerable<MovieResponse> GetTopRatedUserMovies(int userId, int count = 5)
        {
            return _movieRepository.GetMovies()
                .Where(m => m.UserRatings.Any(r => r.UserId == userId))
                .Select(MapToMovieResponse)
                .OrderByDescending(m => m.AverageRating)
                .ThenBy(m => m.Title)
                .Take(count)
                .ToList();
        }

        public async Task<UpdateRatingResponse> RateMovie(UserRatingUpdateRequest userRatingUpdateRequest)
        {
            var updateRatingResponse = new UpdateRatingResponse
            {
                MovieId = userRatingUpdateRequest.MovieId
            };

            var movieToUpdate = await _movieRepository.GetMovie(userRatingUpdateRequest.MovieId);

            if (movieToUpdate == null)
            {
                updateRatingResponse.RatingUpdateStatus = RatingUpdateStatus.NotFound;
                return updateRatingResponse;
            }

            var userRating =
                await _movieRepository.GetUserRating(userRatingUpdateRequest.MovieId, userRatingUpdateRequest.UserId);
            if (userRating != null)
            {
                userRating.Rating = userRatingUpdateRequest.Rating;
                await _movieRepository.UpdateMovieRating(userRating);
                updateRatingResponse.RatingUpdateStatus = RatingUpdateStatus.Updated;
            }
            else
            {
                await _movieRepository.AddMovieRating(userRatingUpdateRequest.MovieId, userRatingUpdateRequest.UserId,
                    userRatingUpdateRequest.Rating);
                updateRatingResponse.RatingUpdateStatus = RatingUpdateStatus.Added;
            }

            return updateRatingResponse;
        }

        private static MovieResponse MapToMovieResponse(Movie movie)
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