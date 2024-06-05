using ImdbWebApi.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<IList<GenderDb>> GetGendersAsync();
        Task<GenderDb> GetGenderAsync(int genderId);
        Task CreateGenderAsync(GenderDb genderDb);
        Task UpdateGenderAsync(GenderDb updatedGender);
        Task DeleteGenderAsync(int genderId);
    }
}
