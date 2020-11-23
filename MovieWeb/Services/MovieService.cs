using AutoMapper;
using MovieWeb.Database;
using MovieWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieWeb.Services
{
    public interface IMovieService
    {
        MovieDto Insert(MovieDto movie);
        IEnumerable<MovieListDto> GetMovies();
        MovieDto GetMovie(int id);
        void Delete(int id);
        void Update(int id, MovieDto movie);
    }

    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieService(MovieDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public MovieDto GetMovie(int id)
        {
            return _mapper.Map<MovieDto>(_dbContext.Movies.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<MovieListDto> GetMovies()
        {
            return _mapper.Map<IEnumerable<MovieListDto>>(_dbContext.Movies.ToList());
        }

        public MovieDto Insert(MovieDto movie)
        {
            _dbContext.Movies.Add(_mapper.Map<Movie>(movie));
            _dbContext.SaveChanges();
            return movie;
        }

        public void Delete(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();
            }
        }

        public void Update(int id, MovieDto updatedMovie)
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Description = updatedMovie.Description;
                movie.ReleaseDate = updatedMovie.ReleaseDate;
                movie.Rating = updatedMovie.Rating;
                movie.Genre = updatedMovie.Genre;
                _dbContext.SaveChanges();
            }
        }
    }
}