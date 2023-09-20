using Core.DTOs;
using Core.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet] //GET: ~/api/movies
        //[HttpGet("all")] //GET: ~/api/movies/all
        //[HttpGet("/movies")] //GET: ~/movies
        public async Task<IActionResult> Get()
        {
         //    Movies.Include(x => x.Genres).ThenInclude(x => x.Genre);
         
            return Ok(await _moviesService.GetAllAsync());
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> Get([FromRoute] int id)  //default FromQuery   => ~/api/movies?id=2   //FromRoute => ~/api/movies/2
        { 
            return Ok(await _moviesService.GetByIdAsync(id));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateMovieDto movie) {
            await _moviesService.CreateAsync(movie);
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(MovieDto movie)
        {
            await _moviesService.EditAsync(movie);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await _moviesService.DeleteAsync(id);
            return Ok();
        }

    }
}
