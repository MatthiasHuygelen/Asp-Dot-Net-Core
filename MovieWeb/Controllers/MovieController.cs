using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Database;
using MovieWeb.Models;
using MovieWeb.Services;
using System;
using System.Linq;

namespace MovieWeb.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieDatabase;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieDatabase , IMapper mapper)
        {
            _movieDatabase = movieDatabase;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var movies = _movieDatabase.GetMovies()
                                       .Select(x => _mapper.Map<MovieListViewModel>(x));

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
                _movieDatabase.Insert( _mapper.Map<MovieDto>(movie));
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var dbMovie = _movieDatabase.GetMovie(id);
            return View(_mapper.Map<EditMovieViewModel>(dbMovie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] EditMovieViewModel movie)
        {
            if (!TryValidateModel(movie))
            {
                return View();
            }

            _movieDatabase.Update(id, _mapper.Map<MovieDto>(movie));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var dbMovie = _movieDatabase.GetMovie(id);
            return View(_mapper.Map<MovieDeleteViewModel>(dbMovie));
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute]int id)
        {
            _movieDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
