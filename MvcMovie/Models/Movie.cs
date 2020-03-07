using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// This will define the table, and the columns that will be defined
/// </summary>
namespace MvcMovie.Models
{
    public class Movie
    {
      
        [Key]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-)]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-)]*$")]
        [StringLength(5)]
        [Required]
        public string Rating { get; set; }

        [Display(Name="DVD Cover")]
        [StringLength(70)]
        public string ImageName { get; set; }

    }

    public enum MovieGenres
    {
        Comedy,
        Family,
        Action,
        Teens,
        Primary
    }
}
