using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{

    public class MoviesController : Controller
    {

        private readonly MvcMovieContext _context;


        // Returns a string list of available genres
        public IEnumerable<string> GetAllGenres()
        {
            return new List<string>
            {
                "Comedy",
                "Action",
                "Family",
                "Kids",
            };
        }

        // This will do the work of taking the string list and turning it into the select list values
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold all the values
            var selectList = new List<SelectListItem>();

            foreach (var genre in elements)
            {
                // Will create your select list value, and the display text for each item
                selectList.Add(new SelectListItem
                {
                    Value = genre,
                    Text = genre
                });
            }

            return selectList;
        }

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString, string sortMethod)
        {
            // Use a linq query to get a list of the genres for the dropdown
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            // Create the linq query to get all the movies
            var movies = from m in _context.Movie
                         select m;

            // If searchString is empty or whitespace, run the query
            if (string.IsNullOrEmpty(searchString))
            {
                if (sortMethod == "default")
                {
                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre));
                    }

                    //else
                    //{
                    //    // Only search by title
                    //    movies = movies.Where(s => s.Title.Contains(searchString));
                    //}
                }

                if (sortMethod == "title")
                {
                    // Run this query if we need to sort by title
                    movies = from m in _context.Movie
                             orderby m.Title ascending
                             select m;

                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre));
                    }

                }

                if (sortMethod == "releaseDate")
                {
                    // Run this query if we need to sort by title
                    movies = from m in _context.Movie
                             orderby m.ReleaseDate ascending
                             select m;

                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre));
                    }

                }
            }

            // If searchString is NOT empty or whitespace, run the query
            if (!string.IsNullOrEmpty(searchString))
            {



                if (sortMethod == "default")
                {
                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre) && (s.Title.Contains(searchString)));
                    }

                    else
                    {
                        // Only search by title
                        movies = movies.Where(s => s.Title.Contains(searchString));
                    }
                }

                if (sortMethod == "title")
                {
                    // Run this query if we need to sort by title
                    movies = from m in _context.Movie
                             orderby m.Title ascending
                             select m;

                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre) && (s.Title.Contains(searchString)));
                    }

                    else
                    {
                        // Only search by title
                        movies = movies.Where(s => s.Title.Contains(searchString));
                    }

                }

                if (sortMethod == "releaseDate")
                {
                    // Run this query if we need to sort by title
                    movies = from m in _context.Movie
                             orderby m.ReleaseDate ascending
                             select m;

                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(s => (s.Genre == movieGenre) && (s.Title.Contains(searchString)));
                    }

                    else
                    {
                        // Only search by title
                        movies = movies.Where(s => s.Title.Contains(searchString));
                    }

                }
            }

            //if(!string.IsNullOrEmpty(movieGenre)) {
            //    movies = movies.Where(s => s.Genre == movieGenre);
            //}

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            // Now return the results to the view
            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            //// Get all dropdown items
            //var movieGenres = GetAllGenres();

            //// Create the model to pass to the page
            //var newMovie = new Movie();

            //// Now create the list of avialable options to be put on the page
            //newMovie.MovieGenres = GetSelectListItems(movieGenres);

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating, ImageName")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price, Rating, ImageName")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
