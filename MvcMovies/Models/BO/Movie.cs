using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovies.Models.BO
{
    public class Movie //mirrors the db table i.e. has the columns in the table defined as properties
    {
        public Movie() { }

        public int ID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType("DataType.Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    //public class MovieDbContext : DbContext
    //{
    //    public DbSet<Movie> Movies { get; set; }
    //}
}