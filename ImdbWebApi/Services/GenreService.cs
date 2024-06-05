using ImdbWebApi.Exceptions;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils.ModelMappers;
using ImdbWebApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IModelMapper _mapper;

        public GenreService(IGenreRepository genreRepository,
            IMovieRepository movieRepository,
            IModelMapper mapper)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        private void ValidateGenre(GenreRequest genreRequest)
        {
            // validate genre name
            StringValidator.Validate("Genre name", genreRequest.Name).Required().MinLength(3).MaxLength(100);
        }

        private async Task<GenreDb> ValidateGetById(int genreId)
        {
            var genreDb = await _genreRepository.GetGenreAsync(genreId) ??
                throw new NotFoundException($"Genre with given id: {genreId} not found.");
            return genreDb;
        }

        private async Task ValidateDelete(int genreId)
        {
            var movies = await _movieRepository.GetMoviesAsync();
            var haveMovieGivenGender = movies.Any(movie =>
            {
                return movie.GenreIds.Split(',').Select(genreId => Convert.ToInt32(genreId)).Any(genre => genre == genreId);
            });

            if (haveMovieGivenGender)
            {
                throw new BadRequestException("Genre cannot be deleted.");
            }
        }

        public async Task CreateGenreAsync(GenreRequest genreRequest)
        {
            ValidateGenre(genreRequest);    
            var newGenre = await _mapper.MapGenreRequestToGenreDb(genreRequest);    
            await _genreRepository.CreateGenreAsync(newGenre);    
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            await ValidateGetById(genreId);
            await ValidateDelete(genreId);
            await _genreRepository.DeleteGenreAsync(genreId);
        }

        public async Task<GenreResponse> GetGenreAsync(int genreId)
        {
            var genreDb = await ValidateGetById(genreId);
            return await _mapper.MapGenreDbToGenreResponse(genreDb);
        }

        public async Task<IList<GenreResponse>> GetGenresAsync()
        {
            var genres = await _genreRepository.GetGenresAsync();
            var list = new List<GenreResponse>(); 
            
            foreach ( var genre in genres )
            {
                list.Add(await _mapper.MapGenreDbToGenreResponse(genre));
            }

            return list;
        }

        public async Task UpdateGenreAsync(int genreId, GenreRequest updatedGenre)
        {
            var genreDb = await ValidateGetById(genreId);
            ValidateGenre(updatedGenre);
            genreDb = await _mapper.MapGenreRequestToGenreDb(updatedGenre);
            genreDb.Id = genreId;
            await _genreRepository.UpdateGenreAsync(genreDb);
        }
    }
}
