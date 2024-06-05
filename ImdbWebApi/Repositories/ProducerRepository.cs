using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class ProducerRepository : BaseRepository<ProducerDb>, IProducerRepository
    {
        public ProducerRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDBDB)
        {
            
        }
        public async Task CreateProducerAsync(ProducerDb producerDb)
        {
            const string query = @"
            INSERT INTO Producers (
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

            await ExecuteQueryAsync(query, producerDb);
        }

        public async Task DeleteProducerAsync(int producerId)
        {
            const string query = @"
            DELETE
            FROM Producers
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, new { Id = producerId });
        }

        public async Task<ProducerDb> GetProducerAsync(int producerId)
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
	            , [DateOfBirth] AS DOB
	            , [Bio]
	            , [GenderId]
            FROM Producers
            WHERE [Id] = @Id";

            return await GetAsync(query, new { Id = producerId });
        }

        public async Task<IList<ProducerDb>> GetProducersAsync()
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
	            , [DateOfBirth] AS DOB
	            , [Bio]
	            , [GenderId]
            FROM Producers";

            var producers = await GetAllAsync(query);
            return producers.ToList();
        }

        public async Task UpdateProducerAsync(ProducerDb updatedProducer)
        {
            const string query = @"
            UPDATE Producers
            SET [Name] = @Name
	            , [DateOfBirth] = @DOB
	            , [Bio] = @Bio
	            , [GenderId] = @GenderId
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, updatedProducer);
        }
    }
}
