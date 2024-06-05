using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetGenresAsync();
            return Ok(new { Data = genres });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            var genreResponse = await _genreService.GetGenreAsync(id);
            return Ok(new { Data = genreResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreRequest genreRequest)
        {
            await _genreService.CreateGenreAsync(genreRequest);
            return CreatedAtAction(null, new { Data = "Genre created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] GenreRequest updatedGenre)
        {
            await _genreService.UpdateGenreAsync(id, updatedGenre);
            return Ok(new { Data = $"Genre with id: {id} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return Ok(new { Data = $"Genre with id: {id} deleted successfully." });
        }
    }
}
