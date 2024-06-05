using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await _actorService.GetActorsAsync();
            return Ok(new { Data = actors });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetActorById([FromRoute] int id)
        {
            var actorResponse = await _actorService.GetActorAsync(id);
            return Ok(new { Data = actorResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] ActorRequest actorRequest)
        {
            await _actorService.CreateActorAsync(actorRequest);
            return CreatedAtAction(null, new { Data = "Actor created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateActor([FromRoute] int id, [FromBody] ActorRequest updateActor)
        {
            await _actorService.UpdateActorAsync(id, updateActor);
            return Ok(new { Data = $"Actor with id: {id} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteActor([FromRoute] int id)
        {
            await _actorService.DeleteActorAsync(id);
            return Ok(new { Data = $"Actor with id: {id} deleted successfully." });
        }   
    }
}
