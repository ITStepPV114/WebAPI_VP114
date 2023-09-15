using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> _repository;
        public MoviesController(IRepository<Movie> repository )
        {
            _repository = repository;
        }

        [HttpGet] //GET: ~/api/movies
        //[HttpGet("all")] //GET: ~/api/movies/all
        //[HttpGet("/movies")] //GET: ~/movies
        public async Task<IActionResult> Get()
        {
         //    Movies.Include(x => x.Genres).ThenInclude(x => x.Genre);
         
            return Ok(await _repository.GetAsync(includeProperties: new[] {"Genres"}));
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> Get([FromRoute] int id)  //default FromQuery   => ~/api/movies?id=2   //FromRoute => ~/api/movies/2
        { 
            return Ok(await _repository.GetByIDAsync(id));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Movie movie) {
            await _repository.InsertAsync(movie);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(Movie movie)
        {
            await _repository.UpdateAsync(movie);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return Ok();
        }

    }
}
