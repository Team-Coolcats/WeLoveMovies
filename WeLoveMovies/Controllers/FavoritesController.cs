using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveMovies.Models;
using WeLoveMovies.Controllers;

namespace WeLoveMovies.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly WeLoveMoviesDbContext _context;

        public FavoritesController(WeLoveMoviesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FavoriteList(string movieId)
        {
            var newFavorite = new Favorites();
            newFavorite.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newFavorite.MovieId = movieId;

            if (ModelState.IsValid)
            {
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();

                return View();
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