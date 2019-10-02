using System;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;

namespace Movies.Infrastructure.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(sc => new {sc.MovieId, sc.GenreId});

            modelBuilder.Entity<Genre>().HasData(
                new Genre {GenreId = 1, Name = "Horror"},
                new Genre {GenreId = 2, Name = "Comedy"},
                new Genre {GenreId = 3, Name = "Action"},
                new Genre {GenreId = 4, Name = "Drama"},
                new Genre {GenreId = 5, Name = "Sci Fi"});

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1, FirstName = "Bob", LastName = "Smith", EmailAddress = "bob@smith.com",
                    Username = "bobbySmith"
                },
                new User
                {
                    UserId = 2, FirstName = "Mary", LastName = "Brown", EmailAddress = "mary@brown.com",
                    Username = "mary-brown"
                },
                new User
                {
                    UserId = 3, FirstName = "Sue", LastName = "Parker", EmailAddress = "sure@gmail.com",
                    Username = "sue99"
                },
                new User
                {
                    UserId = 4, FirstName = "James", LastName = "Windsor", EmailAddress = "j99@mail.com",
                    Username = "j88windsor"
                });

            modelBuilder.Entity<UserRating>().HasData(
                new UserRating {UserRatingId = 1, UserId = 1, Rating = 5, MovieId = 1},
                new UserRating {UserRatingId = 2, UserId = 2, Rating = 3, MovieId = 1},
                new UserRating {UserRatingId = 3, UserId = 3, Rating = 3, MovieId = 1},
                new UserRating {UserRatingId = 4, UserId = 1, Rating = 5, MovieId = 3});

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1, Title = "The Shining", ReleaseDate = new DateTime(1980, 10, 2),
                    RunningTimeInMinutes = 144
                },
                new Movie
                {
                    MovieId = 2, Title = "Dumb and Dumber", ReleaseDate = new DateTime(1995, 04, 7),
                    RunningTimeInMinutes = 107
                },
                new Movie
                {
                    MovieId = 3, Title = "True Lies", ReleaseDate = new DateTime(1994, 08, 12),
                    RunningTimeInMinutes = 141
                },
                new Movie
                {
                    MovieId = 4, Title = "The Martian", ReleaseDate = new DateTime(2015, 09, 30),
                    RunningTimeInMinutes = 144
                },
                new Movie
                {
                    MovieId = 5, Title = "Avatar", ReleaseDate = new DateTime(2009, 12, 17), RunningTimeInMinutes = 162
                },
                new Movie
                {
                    MovieId = 6, Title = "Avengers Assemble", ReleaseDate = new DateTime(2012, 04, 26),
                    RunningTimeInMinutes = 143
                });

            modelBuilder.Entity<MovieGenre>().HasData(
                new MovieGenre {MovieId = 1, GenreId = 1},
                new MovieGenre {MovieId = 1, GenreId = 4},
                new MovieGenre {MovieId = 2, GenreId = 2},
                new MovieGenre {MovieId = 3, GenreId = 2},
                new MovieGenre {MovieId = 3, GenreId = 3},
                new MovieGenre {MovieId = 4, GenreId = 5},
                new MovieGenre {MovieId = 5, GenreId = 4},
                new MovieGenre {MovieId = 5, GenreId = 5},
                new MovieGenre {MovieId = 6, GenreId = 3});
        }
    }
}