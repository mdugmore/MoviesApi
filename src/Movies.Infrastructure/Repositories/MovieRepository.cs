using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Infrastructure.Contexts;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _context;

        public MovieRepository(MoviesContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public Task<Movie> GetMovie(int movieId)
        {
            return _context.Movies
                .Include(ur => ur.UserRatings)
                .Include(mg => mg.MovieGenres)
                .SingleOrDefaultAsync(m => m.MovieId == movieId);
        }

        public Task<UserRating> GetUserRating(int movieId, int userId)
        {
            return _context.UserRatings.FirstOrDefaultAsync(ur => ur.MovieId == movieId && ur.UserId == userId);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies
                .Include(ur => ur.UserRatings)
                .Include(mg => mg.MovieGenres)
                .ThenInclude(g => g.Genre);
        }

        public Task<int> UpdateMovieRating(UserRating userRating)
        {
            _context.Entry(userRating).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<int> AddMovieRating(int movieId, int userId, int rating)
        {
            _context.UserRatings.AddAsync(new UserRating {MovieId = movieId, Rating = rating, UserId = userId});
            return _context.SaveChangesAsync();
        }
    }
}