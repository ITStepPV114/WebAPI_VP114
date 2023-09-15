using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class MovieGenre
    {
        //composition  keys: MoveId+GenreId
        public int MoveId { get; set; }
        public  int GenreId { get; set;}

        public Movie? Movie { get; set;}
        public Genre? Genre { get; set; }
    }
}
