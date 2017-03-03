using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMovies.Models.BO;
using MvcMovies.Models.DL;

namespace MvcMovies.Models.BL
{
    public class MovieManager
    {
        public static IEnumerable<Movie> GetList()
        {
            return MovieDB.GetMovies();
        }

        public static Movie GetMovieById(int id)
        {
            return MovieDB.GetMovieById(id);
        }

        public static int Save(Movie movie)
        {
            return MovieDB.Save(movie);
        }

        internal static bool Delete(int id)
        {
            return MovieDB.Delete(id);
        }
    }
}