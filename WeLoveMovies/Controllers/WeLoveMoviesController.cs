using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveMovies.Models;

namespace WeLoveMovies.Controllers
{
    public class WeLoveMoviesController : Controller
    {
        private readonly WeLoveMoviesDbContext _context;

        public WeLoveMoviesController(WeLoveMoviesDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string erwin = "Erwin";
            string kyle = "bye";
            var myMovie = new Movie();
            return View();
        }
    }
}