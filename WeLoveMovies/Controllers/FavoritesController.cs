using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveMovies.Models;
using WeLoveMovies.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace WeLoveMovies.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly WeLoveMoviesDbContext _context;
        private readonly string _apiKey;

        public FavoritesController(WeLoveMoviesDbContext context, IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("APIKeys")["APINameKey"];
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> ViewFavoritesAsync()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Favorites> thisUserFaves = _context.Favorites.Where(x => x.UserId == id).ToList();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com/");

            List<MovieDetail> favoriteList = new List<MovieDetail>();

            for (int i = 0; i < thisUserFaves.Count; i++)
            {
                var response = await client.GetAsync($"?apikey={_apiKey}&i={thisUserFaves[i].MovieId}");
                var movieDetail = await response.Content.ReadAsAsync<MovieDetail>();
                favoriteList.Add(movieDetail);
            }
            return View(favoriteList);
        }

        public string GetMovieInfo (string movieId)
        {
            return "";
        }

        [HttpGet]
        public IActionResult AddToFavorites()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToFavorites(string imdbID)
        {
            var newFavorite = new Favorites();
            newFavorite.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newFavorite.MovieId = imdbID;

            if (ModelState.IsValid)
            {
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();

                return RedirectToAction("ViewFavorites");
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteFavorite(int id)
        {
            Favorites foundFavorite = _context.Favorites.Find(id);
            if (foundFavorite != null)
            {
                _context.Remove(foundFavorite);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}