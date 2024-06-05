using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/producers")]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducers()
        {
            var producers = await _producerService.GetProducersAsync();
            return Ok(new { Data = producers });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProducerById([FromRoute] int id)
        {
            var producerResponse = await _producerService.GetProducerAsync(id);
            return Ok(new { Data = producerResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducer([FromBody] ProducerRequest producerRequest)
        {
            await _producerService.CreateProducerAsync(producerRequest);
            return CreatedAtAction(null, new { Data = "Producer created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProducer([FromRoute] int id, ProducerRequest updatedProducer)
        {
            await _producerService.UpdateProducerAsync(id, updatedProducer);
            return Ok(new { Data = $"Producer with id: {id} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProducer([FromRoute] int id)
        {
            await _producerService.DeleteProducerAsync(id);
            return Ok(new { Data = $"Producer with id: {id} deleted successfully." });
        }
    }
}
