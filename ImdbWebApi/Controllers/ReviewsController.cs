using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/movies/{movieId:int}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews([FromRoute] int movieId)
        {
            var reviews = await _reviewService.GetReviewsAsync(movieId);
            return Ok(new { Data = reviews });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReview([FromRoute] int movieId, [FromRoute] int id)
        {
            var reviewResponse = await _reviewService.GetReviewAsync(movieId, id);
            return Ok(new { Data = reviewResponse });
        }

        [HttpGet("~/reviews/{id:int}")]
        public async Task<IActionResult> GetReviewById([FromRoute] int id)
        {
            var reviewResponse = await _reviewService.GetReviewByIdAsync(id);
            return Ok(new { Data = reviewResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromRoute] int movieId, [FromBody] ReviewRequest reviewRequest)
        {
            await _reviewService.CreateReviewAsync(movieId, reviewRequest);
            return CreatedAtAction(null, new { Data = $"Review for movie id: {movieId} created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateReview([FromRoute] int movieId, [FromRoute] int id, [FromBody] ReviewRequest updatedReview)
        {
            await _reviewService.UpdateReviewAsync(movieId, id, updatedReview);
            return Ok(new { Data = $"Review with given id: {id} for movieId: {movieId} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int movieId, [FromRoute] int id)
        {
            await _reviewService.DeleteReviewAsync(movieId, id);
            return Ok(new { Data = $"Review with given id: {id} for movieId: {movieId} deleted successfully." });
        }
    }
}
