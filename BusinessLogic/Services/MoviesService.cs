using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Specifications;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;
using System.Net;

namespace Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IRepository<Movie> _repoMovie;
        private readonly IRepository<Genre> _repoGenre;
        private readonly IRepository<MovieGenre> _repoMovieGenre;
        private readonly IMapper _mapper;

        public MoviesService(IRepository<Movie> repoMovie,
                             IRepository<Genre> repoGenre,
                             IRepository<MovieGenre> repoMovieGenre,
                             IMapper mapper)
        {
            _repoMovie = repoMovie;
            _repoGenre = repoGenre;
            _repoMovieGenre = repoMovieGenre;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateMovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _repoMovie.InsertAsync(movie);
            await _repoMovie.SaveAsync();

            //after SaveASync we can get movie id
            if (movieDto.GenreIds != null)
            {
                foreach (var genreId in movieDto.GenreIds)
                {
                    await _repoMovieGenre.InsertAsync(new MovieGenre()
                    {
                        MovieId = movie.Id,
                        GenreId = genreId
                    });

                }
                await _repoMovieGenre.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (_repoMovie.GetByIDAsync(id) == null)
                return;
            await _repoMovie.DeleteAsync(id);
            await _repoMovie.SaveAsync();
        }

        public async Task EditAsync(MovieDto movie)
        {
            await _repoMovie.UpdateAsync(_mapper.Map<Movie>(movie));
            await _repoMovie.SaveAsync();
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync()
        {
            // var movies= await _repoMovie.GetAsync(orderBy: x=>x.OrderByDescending(m=>m.Title), includeProperties: new[] { "Genres" });
            var movies = await _repoMovie.GetListBySpec(new MoviesSpec.OrderedAll());
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetByIdAsync(int id)
        {
            var movie = await _repoMovie.GetItemBySpec(new MoviesSpec.ById(id));
            if (movie == null)
                //throw new CustomHttpException($"Movie by {id} not found!", HttpStatusCode.NotFound);
                throw new CustomHttpException(ErrorMessages.MovieNotFoundById, HttpStatusCode.NotFound);
            //throw new HttpRequestException("Not Found");
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            List<Genre> genres = (await _repoGenre.GetAsync()).ToList();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);


        }
    }
}
