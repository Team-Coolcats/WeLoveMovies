using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeLoveMovies.Models;

namespace WeLoveMovies.Controllers
{
    public class WeLoveMoviesController : Controller
    {
        private readonly WeLoveMoviesDbContext _context;
        private readonly string _apiKey;
        public WeLoveMoviesController(WeLoveMoviesDbContext context, IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("APIKeys")["APINameKey"];
            _context = context;
        }

        public IActionResult Index()
        {
            string heather = "hi";
            var myMovie = new Movie();
            return View();
        }
    }
}