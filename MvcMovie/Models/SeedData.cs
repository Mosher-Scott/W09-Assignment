using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;

namespace MvcMovie.Models
{
    // You will need to add this to the Program.cs file so the DB will be seeded
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies in the DB already
                if (context.Movie.Any())
                {
                    return;   // DB already has data, so don't add anything
                }

                // Now if there is nothing, add in all the movies in this range
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "Saints & Soldiers",
                        ReleaseDate = DateTime.Parse("2005-3-25"),
                        Genre = "Action",
                        Price = 7.99M,
                        Rating = "PG-13",
                        ImageName = "Saints-and-Soldiers.jpg"
                    },

                    new Movie
                    {
                        Title = "The Saratov Approach",
                        ReleaseDate = DateTime.Parse("2013-10-9"),
                        Genre = "Action",
                        Price = 8.99M,
                        Rating = "PG-13",
                        ImageName = "The-Saratov-Approach.jpg"
                    },

                    new Movie
                    {
                        Title = "The Singles Ward",
                        ReleaseDate = DateTime.Parse("2002-01-30"),
                        Genre = "Comedy",
                        Price = 9.99M,
                        Rating = "PG",
                        ImageName = "Singles-Ward.jpg"
                    },

                    new Movie
                    {
                        Title = "The Best Two Years",
                        ReleaseDate = DateTime.Parse("2004-02-20"),
                        Genre = "Comedy",
                        Price = 3.99M,
                        Rating = "PG",
                        ImageName = "Best-Two-Years.jpg"
                    }
                );

                // Save the changes, otherwise you just wasted your time
                context.SaveChanges();
            }
        }
    }
}
