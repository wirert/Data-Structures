using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MovieDatabase
{
    public class MovieDatabase : IMovieDatabase
    {
        private readonly HashSet<Movie> movies = new HashSet<Movie>();
        private readonly HashSet<Actor> actors = new HashSet<Actor>();

        public void AddActor(Actor actor) => actors.Add(actor);

        public void AddMovie(Actor actor, Movie movie)
        {
            if (!Contains(actor))
            {
                throw new ArgumentException();
            }

            if (!Contains(movie))
            {
                movies.Add(movie);
            }

            actor.Movies.Add(movie);
        }

        public bool Contains(Actor actor) => actors.Contains(actor);

        public bool Contains(Movie movie) => movies.Contains(movie);

        public IEnumerable<Actor> GetActorsOrderedByMaxMovieBudgetThenByMoviesCount()
                    => actors.OrderByDescending(a => a.Movies.Last())
                             .ThenByDescending(a => a.Movies.Count)
                             .ToHashSet();

        public IEnumerable<Movie> GetAllMovies() => movies;

        public IEnumerable<Movie> GetMoviesInRangeOfBudget(double lower, double upper)
            => movies.Where(m => m.Budget >= lower && m.Budget <= upper)
                    .OrderByDescending(m => m.Rating)
                    .ToArray();

        public IEnumerable<Movie> GetMoviesOrderedByBudgetThenByRating()
            => movies.OrderByDescending(a => a.Budget)
                     .ThenByDescending(m => m.Rating)
                     .ToArray();

        public IEnumerable<Actor> GetNewbieActors() 
                    => actors.Where(a => a.Movies.Count == 0);
    }
}
