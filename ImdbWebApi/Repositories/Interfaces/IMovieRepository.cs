using ImdbWebApi.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IList<MovieDb>> GetMoviesGivenYearAsync(int year);
        Task<IList<MovieDb>> GetMoviesAsync();
        Task<MovieDb> GetMovieAsync(int movieId);
        Task CreateMovieAsync(MovieDb movieDb);
        Task UpdateMovieAsync(MovieDb updatedMovie);
        Task DeleteMovieAsync(int movieId);
    }
}
