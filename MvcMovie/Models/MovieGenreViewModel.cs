using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        // A list of movies
        public List<Movie> Movies { get; set; }

        // Will contain all the genres to be put into a dropdown list
        public SelectList Genres { get; set; }

        // Contains the genre the person selected
        public string MovieGenre { get; set; }

        // The user entered search string
        public string SearchString { get; set; }
    }
}
