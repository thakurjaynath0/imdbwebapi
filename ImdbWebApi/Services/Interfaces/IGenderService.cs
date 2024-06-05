using ImdbWebApi.Models;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IGenderService
    {
        public Task<IList<GenderResponse>> GetGendersAsync();
        public Task<GenderResponse> GetGenderAsync(int genderId);
        public Task CreateGenderAsync(GenderRequest gender);
        public Task UpdateGenderAsync(int genderId, GenderRequest updatedGender);
        public Task DeleteGenderAsync(int genderId);
    }
}
