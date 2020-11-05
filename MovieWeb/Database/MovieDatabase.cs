using MovieWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieWeb.Database
{
    public interface IMovieDatabase
    {
        Movie Insert(Movie movie);
        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int id);
        void Delete(int id);
        void Update(int id, Movie movie);
    }

    public class MovieDatabase : IMovieDatabase
    {
        private int _counter;
        private readonly List<Movie> _movies;

        public MovieDatabase()
        {
            if (_movies == null)
            {
                _movies = new List<Movie>();
            }
        }

        public Movie GetMovie(int id)
        {
            return _movies.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movies;
        }

        public Movie Insert(Movie movie)
        {
            _counter++;
            movie.Id = _counter;
            _movies.Add(movie);
            return movie;
        }

        public void Delete(int id)
        {
            var movie = _movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
            }
        }

        public void Update(int id, Movie updatedMovie)
        {
            var movie = _movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Description = updatedMovie.Description;
                movie.ReleaseDate = updatedMovie.ReleaseDate;
                movie.Rating = updatedMovie.Rating;
                movie.Genre = updatedMovie.Genre;
            }
        }
    }
}