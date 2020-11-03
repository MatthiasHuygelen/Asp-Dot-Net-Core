using Microsoft.AspNetCore.Mvc;
using MovieWeb.Database;
using MovieWeb.Models;
using System;
using System.Linq;

namespace MovieWeb.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieDatabase _movieDatabase;

        public MovieController(IMovieDatabase movieDatabase)
        {
            _movieDatabase = movieDatabase;
        }

        public IActionResult Index()
        {
            var movies = _movieDatabase.GetMovies()
                                       .Select(x => new MovieListViewModel { Title = x.Title, Description = x.Description, Id = x.Id });

            return View(movies);
        }

        public IActionResult Detail([FromRoute] int id)
        {
            var movie = _movieDatabase.GetMovie(id);

            return View(new MovieDetailViewModel
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating
            }
            );
        }

        public IActionResult Create()
        {
            MovieCreateViewModel vm = new MovieCreateViewModel();
            vm.ReleaseDate = DateTime.Now;
            vm.Rating = 1;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create([FromForm] MovieCreateViewModel movie)
        {
            if (TryValidateModel(movie))
            {
                _movieDatabase.Insert(new Movie
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    Genre = movie.Genre,
                    Rating = movie.Rating,
                    ReleaseDate = movie.ReleaseDate
                });
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
