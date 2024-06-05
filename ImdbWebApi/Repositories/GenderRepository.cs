using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class GenderRepository : BaseRepository<GenderDb>, IGenderRepository
    {

        public GenderRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDBDB)
        {
        }

        public async Task CreateGenderAsync(GenderDb genderDb)
        {
            const string query = @"
            INSERT INTO Genders (Name)
            VALUES (@Name)";

            await ExecuteQueryAsync(query, genderDb);
        }

        public async Task DeleteGenderAsync(int genderId)
        {
            const string query = @"
            DELETE
            FROM Genders
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, new { Id = genderId });
        }

        public async Task<GenderDb> GetGenderAsync(int genderId)
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
            FROM Genders
            WHERE [Id] = @Id";
            
            return await GetAsync(query, new { Id = genderId });    
        }

        public async Task<IList<GenderDb>> GetGendersAsync()
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
            FROM Genders";

            var genders = await GetAllAsync(query);
            return genders.ToList();
        }

        public async Task UpdateGenderAsync(GenderDb updatedGender)
        {
            const string query = @"
            UPDATE Genders
            SET [Name] = @Name
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, updatedGender);
        }
    }
}
