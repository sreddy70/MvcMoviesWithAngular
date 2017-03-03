using MvcMovies.Models.BL;
using MvcMovies.Models.BO;
using System.Net;
using System.Web.Mvc;

/// <summary>
/// Author: Sankalp
/// Date: June 2016
/// This controller has been modified to use its own Business and Data layers.
/// The intention is NOT to use the default Entity Framework provided by the ASP.Net MVC scaffolding.
/// The EF framework does not allow us to control data access optimally (ie. either via queries, sp), hence this change.
/// Calls from the controller to access the db now go through the following layers: Controller -> Business layer -> Data layer.
/// </summary>
namespace MvcMovies.Controllers
{

    public class MoviesController : Controller
    {
        //private MovieDbContext db = new MovieDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            //return View(db.Movies.ToList());

            //new
            return View(MovieManager.GetList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            //new
            Movie movie = MovieManager.GetMovieById(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, ReleaseDate, Genre, Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //db.Movies.Add(movie);
                //db.SaveChanges();
                MovieManager.Save(movie);

                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //
            //Movie movie = db.Movies.Find(id);
            Movie movie = MovieManager.GetMovieById(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Title, ReleaseDate, Genre, Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(movie).State = EntityState.Modified;
                //db.SaveChanges();
                MovieManager.Save(movie);

                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            Movie movie = MovieManager.GetMovieById(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Movie movie = db.Movies.Find(id);
            //db.Movies.Remove(movie);
            //db.SaveChanges();
            MovieManager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
