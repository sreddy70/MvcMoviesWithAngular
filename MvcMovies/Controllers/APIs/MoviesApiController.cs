using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcMovies.Models.BL;
using MvcMovies.Models.BO;

namespace MvcMovies.Api.Controllers
{
    [RoutePrefix("movieapi/Movies")]
    public class MoviesController : ApiController
    {
        [Route("All")]
        [HttpGet]
        public IEnumerable<Movie> GetList()
        {
            //try
            //{
                return MovieManager.GetList();
            //}
            //catch
            //{

            //}
        }

        /* uri format: 
            [Route("ById")] supports uri -> moviesapi/Movies/ById?id=1
            [Route("ById/{id:int}")] supports uri -> moviesapi/Movies/ById/1
         Route attributes use the HttpAttributeRoutes (see webapiconfig.cs)
         The default convention (movies/Movies/By/1) does Not work since the 
         method name is not of the format "Getxxxx"
        */
        [Route("ById/{id:int}")]
        [HttpGet]
        public IHttpActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = MovieManager.GetMovieById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [Route("CreateNew")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieManager.Save(movie);
                return Ok();
            }
            return BadRequest();
        }

        [Route("Update")]
        [HttpPost] //using HttpPost since we will be sending the data in the body
        public IHttpActionResult Edit([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieManager.Save(movie);
                return Ok();
            }
            return BadRequest();
        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (MovieManager.Delete(id.Value))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
