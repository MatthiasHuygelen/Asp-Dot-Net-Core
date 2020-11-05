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
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate
            }
            );
        }

        public IActionResult Create()
        {
            MovieCreateViewModel vm = new MovieCreateViewModel();
            vm.ReleaseDate = DateTime.Now;
            vm.Rating = 3;
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

        public IActionResult Edit([FromRoute] int id)
        {
            var dbMovie = _movieDatabase.GetMovie(id);

            EditMovieViewModel vm = new EditMovieViewModel();
            vm.Title = dbMovie.Title;
            vm.Description = dbMovie.Description;
            vm.ReleaseDate = dbMovie.ReleaseDate;
            vm.Genre = dbMovie.Genre;
            vm.Rating = dbMovie.Rating;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] EditMovieViewModel movie)
        {
            if (!TryValidateModel(movie))
            {
                return View();
            }

            _movieDatabase.Update(id, new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
            });

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var dbMovie = _movieDatabase.GetMovie(id);

            var vm = new MovieDeleteViewModel();
            vm.Id = dbMovie.Id;
            vm.Title = dbMovie.Title;

            return View(vm);
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute]int id)
        {
            _movieDatabase.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
