using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        
        public ICollection<MovieGenre>? Genres { get; }
    }
}
