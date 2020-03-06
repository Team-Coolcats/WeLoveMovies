using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeLoveMovies.Models;

namespace WeLoveMovies.Controllers
{
    public class SearchController : Controller
    {

        private readonly WeLoveMoviesDbContext _context;
        private readonly string _apiKey;
        public SearchController(WeLoveMoviesDbContext context, IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("APIKeys")["APINameKey"];
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //so, for this fella, i think best approach would be:
        //  Just call the action 'Search', and use various case statements to determine search type
        //  Must consider ways to do "multi search" i.e. search by title && year
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string title)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com/");
         
            var response = await client.GetAsync($"?apikey={_apiKey}&s={title}");

            //works
            //var movies1 = await response.Content.ReadAsStringAsync();
       
            var movies = await response.Content.ReadAsAsync<MovieRootObject>();

            return View("DisplaySearchResults", movies);
        }

        [HttpGet]
        public IActionResult DisplaySearchResults()
        {
            return View();
        }

        //AddToFavorites()
    }
}