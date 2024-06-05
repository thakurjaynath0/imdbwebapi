using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class ActorRepository : BaseRepository<ActorDb>, IActorRepository
    {
        public ActorRepository(IOptions<ConnectionString> options)
            :base(options.Value.IMDBDB)
        {
        }

        public async Task CreateActorAsync(ActorDb actorDb)
        {
            const string query = @"
            INSERT INTO Actors (
	              Name
	            , DateOfBirth
	            , Bio
	            , GenderId
	            )
            VALUES (
	              @Name
	            , @DOB
	            , @Bio
	            , @GenderId
	            )";

            await ExecuteQueryAsync(query, actorDb);
        }

        public async Task DeleteActorAsync(int actorId)
        {
            const string query = @"
            DELETE
            FROM Actors
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, new { Id = actorId }); 
        }

        public async Task<ActorDb> GetActorAsync(int actorId)
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
	            , [DateOfBirth] AS DOB
	            , [Bio]
	            , [GenderId]
            FROM Actors
            WHERE [Id] = @Id";

            return await GetAsync(query, new { Id = actorId });
        }

        public async Task<IList<ActorDb>> GetActorsAsync()
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
	            , [DateOfBirth] AS DOB
	            , [Bio]
	            , [GenderId]
            FROM Actors";

            var actors = await GetAllAsync(query);
            return actors.ToList();
        }

        public async Task UpdateActorAsync(ActorDb updatedactor)
        {
            const string query = @"
            UPDATE Actors
            SET [Name] = @Name
	            , [DateOfBirth] = @DOB
	            , [Bio] = @Bio
	            , [GenderId] = @GenderId
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, updatedactor);
        }
    }
}
