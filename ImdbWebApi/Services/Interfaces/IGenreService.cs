using System.Collections.Generic;
using System.Threading.Tasks;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IList<GenreResponse>> GetGenresAsync();
        Task<GenreResponse> GetGenreAsync(int genreId);
        Task CreateGenreAsync(GenreRequest genre);
        Task UpdateGenreAsync(int genreId, GenreRequest updatedGenre);
        Task DeleteGenreAsync(int genreId);
    }
}