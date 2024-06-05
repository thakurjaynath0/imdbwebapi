using System;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils.ModelMappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImdbWebApi.Exceptions;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Validators;

namespace ImdbWebApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IModelMapper _mapper;

        public MovieService(IMovieRepository movieRepository,
            IActorRepository actorRepository,
            IProducerRepository producerRepository,
            IGenreRepository genreRepository,
            IReviewRepository reviewRepository,
            IModelMapper mapper)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _producerRepository = producerRepository;
            _genreRepository = genreRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        private async Task ValidateMovie(MovieRequest movieRequest)
        {
            // validate movie name
            StringValidator.Validate("Movie name", movieRequest.Name).Required().MinLength(3).MaxLength(50);
            //validate year of release
            IntegerValidator.Validate("Year of release", movieRequest.YearOfRelease).MinValue(1888).MaxValue(DateTime.Now.Year);
            // validate movie plot
            StringValidator.Validate("Plot", movieRequest.Plot).Required().MinLength(30).MaxLength(1000);
            // validate producer id
            var producerDb = await _producerRepository.GetProducerAsync(movieRequest.ProducerId) ??
                throw new NotFoundException($"Producer with given id: {movieRequest.ProducerId} not found.");
            // validate cover image
            StringValidator.Validate("Cover image", movieRequest.CoverImage).Required().MaxLength(2048);

            // validate actor ids
            if (movieRequest.ActorIds == null || movieRequest.ActorIds.Count == 0)
            {
                throw new BadRequestException("Actors should not be null or empty.");
            }

            foreach (int actorId in movieRequest.ActorIds)
            {
                var actorDb = await _actorRepository.GetActorAsync(actorId) ??
                    throw new NotFoundException($"Actor with given id: {actorId} not found.");
            }

            // validate genre ids
            if (movieRequest.GenreIds == null || movieRequest.GenreIds.Count == 0)
            {
                throw new BadRequestException("Genres should not be null or empty.");
            }

            foreach (int genreId in movieRequest.GenreIds)
            {
                var genreDb = await _genreRepository.GetGenreAsync(genreId) ??
                    throw new NotFoundException($"Genre with given id: {genreId} not found.");
            }
        }

        private async Task<MovieDb> ValidateGetById(int movieId)
        {
            var movieDb = await _movieRepository.GetMovieAsync(movieId) ??
                throw new NotFoundException($"Movie with given id: {movieId} not found.");
            return movieDb;
        }

        private async Task ValidateDelete(int movieId)
        {
            var doMovieHaveReviews = await _reviewRepository.GetReviewsAsync(movieId);

            if (doMovieHaveReviews.Count != 0)
            {
                throw new BadRequestException("Movie cannot be deleted.");
            }
        }

        public async Task CreateMovieAsync(MovieRequest movieRequest)
        {
            await ValidateMovie(movieRequest);
            var newMovie = await _mapper.MapMovieRequestToMovieDb(movieRequest);    
            await _movieRepository.CreateMovieAsync(newMovie);
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            await ValidateGetById(movieId);
            await ValidateDelete(movieId);
            await _movieRepository.DeleteMovieAsync(movieId);
        }

        public async Task<MovieResponse> GetMovieAsync(int movieId)
        {
            var movieDb = await ValidateGetById(movieId);
            return await _mapper.MapMovieDbToMovieResponse(movieDb);
        }

        public async Task<IList<MovieResponse>> GetMoviesGivenYearAsync(int year)
        {
            var movies = await _movieRepository.GetMoviesGivenYearAsync(year);
            var list = new List<MovieResponse>();

            foreach (var movie in movies)
            {
                list.Add(await _mapper.MapMovieDbToMovieResponse(movie));
            }

            return list;
        }

        public async Task<IList<MovieResponse>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            var list = new List<MovieResponse>();

            foreach (var movie in movies)
            {
                list.Add(await _mapper.MapMovieDbToMovieResponse(movie));
            }

            return list;
        }

        public async Task UpdateMovieAsync(int movieId, MovieRequest updatedMovie)
        {
            var movieDb = await ValidateGetById(movieId);
            await ValidateMovie(updatedMovie);
            movieDb = await _mapper.MapMovieRequestToMovieDb(updatedMovie);
            movieDb.Id = movieId;   
            await _movieRepository.UpdateMovieAsync(movieDb);
        }
    }
}
