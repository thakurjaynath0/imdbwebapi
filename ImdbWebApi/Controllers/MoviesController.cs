using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies([FromQuery] int year)
        {
            var movies = await _movieService.GetMoviesGivenYearAsync(year);
            return Ok(new { Data = movies });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            var movieResponse = await _movieService.GetMovieAsync(id);
            return Ok(new { Data = movieResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieRequest movieRequest)
        {
            await _movieService.CreateMovieAsync(movieRequest);
            return CreatedAtAction(null, new { Data = "Movie created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] MovieRequest updateMovies)
        {
            await _movieService.UpdateMovieAsync(id, updateMovies);
            return Ok(new { Data = $"Movie with id: {id} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return Ok(new { Data = $"Movie with id: {id} deleted successfully." });
        }
    }
}
