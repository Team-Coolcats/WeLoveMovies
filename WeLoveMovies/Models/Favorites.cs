using System;
using System.Collections.Generic;

namespace WeLoveMovies.Models
{
    public partial class Favorites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
    }
}
