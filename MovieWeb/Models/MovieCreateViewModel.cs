using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieWeb.Models
{
    public class MovieCreateViewModel
    {
        [DisplayName("Titel")]
        [Required, MinLength(1), MaxLength(30)]
        public string Title { get; set; }

        [DisplayName("Omschrijving")]
        [MaxLength(250)]
        public string Description { get; set; }

        [DisplayName("Genre")]
        [MaxLength(20)]
        public string Genre { get; set; }

        [DisplayName("Datum van publicatie")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2070")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Rating")]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
