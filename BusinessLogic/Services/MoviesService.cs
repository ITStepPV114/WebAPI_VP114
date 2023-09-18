using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IRepository<Movie> _repoMovie;
        private readonly IRepository<Genre> _repoGenre;
        private readonly IRepository<MovieGenre> _repoMovieGenre;

        public MoviesService(IRepository<Movie> repoMovie,
                             IRepository<Genre> repoGenre,
                             IRepository<MovieGenre> repoMovieGenre)
        {
            _repoMovie = repoMovie;
            _repoGenre = repoGenre;
            _repoMovieGenre = repoMovieGenre;
        }
        public async Task CreateAsync(Movie movie)
        {
            await _repoMovie.InsertAsync(movie);
            await _repoMovie.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (_repoMovie.GetByIDAsync(id) == null)
                return;
            await _repoMovie.DeleteAsync(id);
            await _repoMovie.SaveAsync();
        }

        public async Task EditAsync(Movie movie)
        {
            await _repoMovie.UpdateAsync(movie);
            await _repoMovie.SaveAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {

            return await _repoMovie.GetAsync(includeProperties: new[] {"Genres"});
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            if ((await _repoMovie.GetByIDAsync(id)) == null)
                return null;
                //throw new HttpRequestException("Not Found");
            return await _repoMovie.GetByIDAsync(id);
        }
    }
}
