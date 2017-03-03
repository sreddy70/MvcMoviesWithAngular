using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovies.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: /HelloWorld/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/
        public ActionResult Welcome(string name, int age = 1)
        {
            //return HttpUtility.HtmlEncode("Hello " + name + ", Your age is: " + age);

            ViewBag.Message = "hello " + name;
            ViewBag.Age = age;

            return View();
        }
    }
}