using System;

namespace MovieWeb.Models
{
    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
    }
}
