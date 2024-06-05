using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class MovieRepository : BaseRepository<MovieDb>, IMovieRepository
    {

        public MovieRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDBDB)
        {

        }

        public async Task CreateMovieAsync(MovieDb movieDb)
        {
            const string sp = "usp_AddMovie";
            await ExecuteStoredProcedureAsync(sp, new
            {
                movieDb.Name,
                movieDb.YearOfRelease,
                movieDb.Plot,
                movieDb.ProducerId,
                movieDb.CoverImage,    
                movieDb.ActorIds,
                movieDb.GenreIds
            });
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            const string sp = "usp_DeleteMovie";
            await ExecuteStoredProcedureAsync(sp, new { MovieId = movieId });
        }

        public async Task<MovieDb> GetMovieAsync(int movieId)
        {
            const string query = @"
            SELECT M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]
	            , STRING_AGG(CONVERT(VARCHAR(max), MA.[ActorId]), ',') AS ActorIds
	            , STRING_AGG(CONVERT(VARCHAR(max), MG.[GenreId]), ',') AS GenreIds
            FROM Movies M
            JOIN Movies_Actors MA ON M.[Id] = MA.[MovieId]
            JOIN Movies_Genres MG ON M.[Id] = MG.[MovieId]
            WHERE M.[Id] = @Id
            GROUP BY M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]";

            var movie = await GetAsync(query, new { Id = movieId });
            if(!string.IsNullOrEmpty(movie?.ActorIds))
            {
                var uniqueActorIds = movie.ActorIds.Split(",").Distinct();
                movie.ActorIds = string.Join(",", uniqueActorIds);
            }

            if(!string.IsNullOrEmpty(movie?.GenreIds))
            {
                var uniqueGenreIds = movie.GenreIds.Split(",").Distinct();
                movie.GenreIds = string.Join(",", uniqueGenreIds);
            }
            
            return movie;
        }

        public async Task<IList<MovieDb>> GetMoviesGivenYearAsync(int year)
        {
            const string query = @"
            SELECT M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]
	            , STRING_AGG(CONVERT(VARCHAR(max), MA.[ActorId]), ',') AS ActorIds
	            , STRING_AGG(CONVERT(VARCHAR(max), MG.[GenreId]), ',') AS GenreIds
            FROM Movies M
            JOIN Movies_Actors MA ON M.[Id] = MA.[MovieId]
            JOIN Movies_Genres MG ON M.[Id] = MG.[MovieId]
            WHERE M.[YearOfRelease] = @Year
            GROUP BY M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]";

            var movies = await GetAllAsync(query, new { Year = year});
            return movies.Select(movie =>
            {
                if (!string.IsNullOrEmpty(movie?.ActorIds))
                {
                    var uniqueActorIds = movie.ActorIds.Split(",").Distinct();
                    movie.ActorIds = string.Join(",", uniqueActorIds);
                }

                if (!string.IsNullOrEmpty(movie?.GenreIds))
                {
                    var uniqueGenreIds = movie.GenreIds.Split(",").Distinct();
                    movie.GenreIds = string.Join(",", uniqueGenreIds);
                }

                return movie;

            }).ToList();
        }

        public async Task<IList<MovieDb>> GetMoviesAsync()
        {
            const string query = @"
            SELECT M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]
	            , STRING_AGG(CONVERT(VARCHAR(max), MA.[ActorId]), ',') AS ActorIds
	            , STRING_AGG(CONVERT(VARCHAR(max), MG.[GenreId]), ',') AS GenreIds
            FROM Movies M
            JOIN Movies_Actors MA ON M.[Id] = MA.[MovieId]
            JOIN Movies_Genres MG ON M.[Id] = MG.[MovieId]
            GROUP BY M.[Id]
	            , M.[Name]
	            , M.[YearOfRelease]
	            , M.[Plot]
	            , M.[ProducerId]
	            , M.[CoverImage]";

            var movies = await GetAllAsync(query);
            return movies.Select(movie =>
            {
                if (!string.IsNullOrEmpty(movie?.ActorIds))
                {
                    var uniqueActorIds = movie.ActorIds.Split(",").Distinct();
                    movie.ActorIds = string.Join(",", uniqueActorIds);
                }

                if (!string.IsNullOrEmpty(movie?.GenreIds))
                {
                    var uniqueGenreIds = movie.GenreIds.Split(",").Distinct();
                    movie.GenreIds = string.Join(",", uniqueGenreIds);
                }

                return movie;

            }).ToList();
        }

        public async Task UpdateMovieAsync(MovieDb updatedMovie)
        {
            const string sp = "usp_UpdateMovie";
            await ExecuteStoredProcedureAsync(sp, new
            {
                MovieId = updatedMovie.Id,
                updatedMovie.Name,
                updatedMovie.YearOfRelease,
                updatedMovie.Plot,
                updatedMovie.ProducerId,
                updatedMovie.CoverImage,
                updatedMovie.ActorIds,
                updatedMovie.GenreIds
            });
        }
    }
}
