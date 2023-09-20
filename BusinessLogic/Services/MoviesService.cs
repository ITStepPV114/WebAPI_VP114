﻿using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private readonly IMapper _mapper;

        public MoviesService(IRepository<Movie> repoMovie,
                             IRepository<Genre> repoGenre,
                             IRepository<MovieGenre> repoMovieGenre,
                             IMapper mapper )
        {
            _repoMovie = repoMovie;
            _repoGenre = repoGenre;
            _repoMovieGenre = repoMovieGenre;
            _mapper = mapper;
        }
        public async Task CreateAsync(MovieDto movie)
        {
            await _repoMovie.InsertAsync(_mapper.Map<Movie>(movie));
            await _repoMovie.SaveAsync();
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
            var movie= await _repoMovie.GetItemBySpec(new MoviesSpec.ById(id));
            if (movie == null)
                return null;
                //throw new HttpRequestException("Not Found");
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
          List<Genre> genres = (await  _repoGenre.GetAsync()).ToList();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);


        }
    }
}
