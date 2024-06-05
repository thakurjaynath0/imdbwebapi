using ImdbWebApi.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IList<GenreDb>> GetGenresAsync();
        Task<GenreDb> GetGenreAsync(int genreId);
        Task CreateGenreAsync(GenreDb genreDb);
        Task UpdateGenreAsync(GenreDb updatedGenre);
        Task DeleteGenreAsync(int genreId);
    }
}
