using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MvcMovies.Models.DL
{
    public class BaseDB
    {
        public DBManager DbManager { get; set; }

        public BaseDB()
        {
            DbManager = new DBManager();
        }
    }
}