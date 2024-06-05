using Microsoft.AspNetCore.Mvc;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace ImdbWebApi.Controllers
{
    [ApiController]
    [Route("/genders")]
    public class GendersController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GendersController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            var genders = await _genderService.GetGendersAsync();
            return Ok(new { Data = genders });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenderById([FromRoute] int id)
        {
            var genderResponse = await _genderService.GetGenderAsync(id);
            return Ok(new { Data = genderResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody] GenderRequest genderRequest)
        {
            await _genderService.CreateGenderAsync(genderRequest);
            return CreatedAtAction(null, new { Data = "Gender created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatedGender([FromRoute] int id, [FromBody] GenderRequest updatedGender)
        {
            await _genderService.UpdateGenderAsync(id, updatedGender);
            return Ok(new { Data = $"Gender with id: {id} updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletedGender([FromRoute] int id)
        {
            await _genderService.DeleteGenderAsync(id);
            return Ok(new { Data = $"Gender with id: {id} deleted successfully." });
        }
    }
}
