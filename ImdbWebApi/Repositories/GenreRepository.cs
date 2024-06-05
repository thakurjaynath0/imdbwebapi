using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class GenreRepository : BaseRepository<GenreDb>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDBDB)
        {
        }

        public async Task CreateGenreAsync(GenreDb genreDb)
        {
            const string query = @"
            INSERT INTO Genres (Name)
            VALUES (@Name)";

            await ExecuteQueryAsync(query, genreDb);
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            const string query = @"
            DELETE
            FROM Genres
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, new { Id = genreId });
        }

        public async Task<GenreDb> GetGenreAsync(int genreId)
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
            FROM Genres
            WHERE [Id] = @Id";

            return await GetAsync(query, new { Id = genreId });
        }

        public async Task<IList<GenreDb>> GetGenresAsync()
        {
            const string query = @"
            SELECT [Id]
	            , [Name]
            FROM Genres";

            var genres = await GetAllAsync(query);
            return genres.ToList();
        }

        public async Task UpdateGenreAsync(GenreDb updatedGenre)
        {
            const string query = @"
            UPDATE Genres
            SET [Name] = @Name
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, updatedGenre);
        }
    }
}
