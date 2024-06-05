using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IList<MovieResponse>> GetMoviesGivenYearAsync(int year);
        Task<IList<MovieResponse>> GetMoviesAsync();
        Task<MovieResponse> GetMovieAsync(int movieId);
        Task CreateMovieAsync(MovieRequest movie);
        Task UpdateMovieAsync(int movieId, MovieRequest updatedMovie);
        Task DeleteMovieAsync(int movieId);
    }
}