using AutoMapper;
using MovieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Profiles
{
    public class MovieProfiles : Profile
    {
        public MovieProfiles()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<MovieListDto, Movie>().ReverseMap();
            CreateMap<MovieListDto, MovieListViewModel>().ReverseMap();
            CreateMap<MovieDto, MovieDetailViewModel>().ReverseMap();
            CreateMap<MovieDto, MovieCreateViewModel>().ReverseMap();
            CreateMap<MovieDto, EditMovieViewModel>().ReverseMap();
        }
    }
}
