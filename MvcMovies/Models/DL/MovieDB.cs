using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMovies.Models.BO;
using System.Data.Common;
using DataAccess;
using System.Data;

namespace MvcMovies.Models.DL
{
    public class MovieDB 
    {
        private static string SelectAllMovies = "SELECT * FROM movies";
        private static string SelectMovieById = "SELECT * FROM movies WHERE id=@id";
        private static string UpdateMovie = "UPDATE movies SET title=@title, genre=@genre, price=@price, releasedate=@releasedate WHERE id=@id;";
        private static string InsertMovie = "INSERT INTO movies (title, genre, price, releasedate) VALUES (@title, @genre, @price, @releasedate);";
        private static string DeleteMovie = "DELETE FROM movies WHERE id=@id";
        
        public static IEnumerable<Movie> GetMovies()
        {
            List<Movie> movieList = null;
            DbDataReader rdr = null;
            DBManager db = null;
            
            try
            {
                db = new DBManager(DBEnum.ProviderType.MySql);
                db.OpenConnection();
                rdr = db.ExecuteReader(SelectAllMovies, System.Data.CommandType.Text);
                if (null != rdr && rdr.HasRows)
                {
                    movieList = new List<Movie>();
                    while (rdr.Read())
                    {
                        movieList.Add(FillDataRecord(rdr));
                    }
                }
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (db.IsOpen) db.CloseConnection();
            }

            return movieList;
        }

        internal static Movie GetMovieById(int id)
        {
            Movie movie = null;
            DbDataReader rdr = null;
            DBManager db = null;

            try
            {
                db = new DBManager(DBEnum.ProviderType.MySql);
                db.OpenConnection();
                db.AddParameter("@id", id);
                rdr = db.ExecuteReader(SelectMovieById, System.Data.CommandType.Text);
                if (rdr.HasRows)
                {
                    if (rdr.Read())
                    {
                        movie = FillDataRecord(rdr);
                    }
                }
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (db.IsOpen) db.CloseConnection();
            }

            return movie;
        }

        internal static int Save(Movie movie)
        {
            DBManager db = null;
            int rowsAffected = 0;

            try
            {
                db = new DBManager(DBEnum.ProviderType.MySql);
                db.OpenConnection();
                db.AddParameter("@title", movie.Title);
                db.AddParameter("@genre", movie.Genre);
                db.AddParameter("@price", movie.Price);
                db.AddParameter("@releasedate", movie.ReleaseDate);
                if (movie.ID <= 0)
                {
                    rowsAffected = db.ExecuteNonQuery(InsertMovie, CommandType.Text); //, DBEnum.DatabaseConnectionState.KeepOpen);
                    //db.CloseConnection();
                }
                else
                {
                    db.AddParameter("@id", movie.ID);
                    rowsAffected = db.ExecuteNonQuery(UpdateMovie, CommandType.Text);
                }
            }
            finally
            {
                if (db.IsOpen) db.CloseConnection();
            }
            return rowsAffected;
        }

        internal static bool Delete(int id)
        {
            DBManager db = null;
            int rowsAffected = 0;

            try
            {
                db = new DBManager(DBEnum.ProviderType.MySql);
                db.OpenConnection();
                db.AddParameter("@id", id);
                rowsAffected = db.ExecuteNonQuery(DeleteMovie, CommandType.Text);
            }
            finally
            {
                if (db.IsOpen) db.CloseConnection();
            }
            return (rowsAffected > 0);
        }

        private static Movie FillDataRecord(IDataReader data)
        {
            return new Movie
            {
                // Mandatory fields
                ID = data.GetInt32(data.GetOrdinal("Id")),
                Title = data.GetString(data.GetOrdinal("Title")),
                Genre = data.GetString(data.GetOrdinal("Genre")),
                Price = data.GetDecimal(data.GetOrdinal("Price")),
                ReleaseDate = data.GetDateTime(data.GetOrdinal("ReleaseDate"))
            };
        }
    }
}