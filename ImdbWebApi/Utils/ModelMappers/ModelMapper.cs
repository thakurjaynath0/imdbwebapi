using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Utils.ModelMappers
{
    public class ModelMapper : IModelMapper
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IGenreRepository _genreRepository;

        public ModelMapper(IGenderRepository genderRepository,
            IActorRepository actorRepository,
            IProducerRepository producerRepository,
            IGenreRepository genreRepository)
        {
            _genderRepository = genderRepository;
            _actorRepository = actorRepository; 
            _producerRepository = producerRepository;   
            _genreRepository = genreRepository; 
        }


        public async Task<ActorResponse> MapActorDbToActorResponse(ActorDb actorDb)
        {
            var gender = await MapGenderDbToGenderResponse(await _genderRepository.GetGenderAsync(actorDb.GenderId));
            var dateOfBirth = actorDb.DOB.ToString("yyyy-MM-dd");

            return new ActorResponse()
            {
                Id = actorDb.Id,
                Name = actorDb.Name,
                DOB = dateOfBirth,
                Bio = actorDb.Bio,
                Gender = gender
            };
        }

        public async Task<ActorDb> MapActorRequestToActorDb(ActorRequest actorRequest)
        {
            var dateOfBirth = DateTimeUtils.ParseFromYMD(actorRequest.DOB);
            return new ActorDb()
            {
                Name = actorRequest.Name,   
                DOB = dateOfBirth,
                Bio = actorRequest.Bio,
                GenderId = actorRequest.GenderId,
            };
        }

        public async Task<GenderResponse> MapGenderDbToGenderResponse(GenderDb genderDb)
        {
            var genderResponse = new GenderResponse()
            {
                Id = genderDb.Id,
                Name = genderDb.Name,
            };

            return genderResponse;
        }

        public async Task<GenderDb> MapGenderRequestToGenderDb(GenderRequest genderRequest)
        {
            return new GenderDb()
            {
                Name = genderRequest.Name,
            };
        }

        public async Task<GenreResponse> MapGenreDbToGenreResponse(GenreDb genreDb)
        {
           return new GenreResponse()
            {
                Id = genreDb.Id,
                Name = genreDb.Name
            };
        }

        public async Task<GenreDb> MapGenreRequestToGenreDb(GenreRequest genreRequest)
        {
            return new GenreDb()
            {
                Name= genreRequest.Name,    
            };
        }

        public async Task<MovieResponse> MapMovieDbToMovieResponse(MovieDb movieDb)
        {
            var producer = await MapProducerDbToProducerResponse(await _producerRepository.GetProducerAsync(movieDb.ProducerId));

            var actorList = new List<ActorResponse>();
            var genreList = new List<GenreResponse>();

            foreach (var actor in movieDb.ActorIds.Split(","))
            {
                try
                {
                    int actorId = Convert.ToInt32(actor);
                    var actorResponse = await MapActorDbToActorResponse(await _actorRepository.GetActorAsync(actorId));
                    actorList.Add(actorResponse);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid type for integer.");
                }
            }
            
            foreach (var genre in movieDb.GenreIds.Split(","))
            {
                try
                {
                    int genreId = Convert.ToInt32(genre);
                    var genreResponse = await MapGenreDbToGenreResponse(await _genreRepository.GetGenreAsync(genreId));
                    genreList.Add(genreResponse);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid type for integer.");
                }
            }

            return new MovieResponse()
            {
                Id = movieDb.Id,
                Name = movieDb.Name,
                YearOfRelease = movieDb.YearOfRelease,
                Plot = movieDb.Plot,
                Producer = producer,
                Actors = actorList,
                Genres = genreList,
                CoverImage = movieDb.CoverImage,
            };
        }

        public async Task<MovieDb> MapMovieRequestToMovieDb(MovieRequest movieRequest)
        {
            var actors = string.Join(",", movieRequest.ActorIds);
            var genres = string.Join(",", movieRequest.GenreIds);

            return new MovieDb()
            {
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                ProducerId = movieRequest.ProducerId,
                ActorIds = actors,
                GenreIds = genres,
                CoverImage = movieRequest.CoverImage,
            };
        }

        public async Task<ProducerResponse> MapProducerDbToProducerResponse(ProducerDb producerDb)
        {
            var gender = await MapGenderDbToGenderResponse(await _genderRepository.GetGenderAsync(producerDb.GenderId)) ;
            var dateOfBirth = producerDb.DOB.ToString("yyyy-MM-dd");

            return new ProducerResponse()
            {
                Id = producerDb.Id,
                Name = producerDb.Name,
                DOB = dateOfBirth,
                Bio = producerDb.Bio,
                Gender = gender
            };
        }

        public async Task<ProducerDb> MapProducerRequestToProducerDb(ProducerRequest producerRequest)
        {
            var dateOfBirth = DateTimeUtils.ParseFromYMD(producerRequest.DOB);
            return new ProducerDb()
            {
                Name = producerRequest.Name,
                DOB = dateOfBirth,
                Bio = producerRequest.Bio,
                GenderId = producerRequest.GenderId
            };
        }

        public async Task<ReviewResponse> MapReviewDbToReviewResponse(ReviewDb reviewDb)
        {
            return new ReviewResponse()
            {
                Id = reviewDb.Id,
                MovieId = reviewDb.MovieId,
                Message = reviewDb.Message
            };
        }

        public async Task<ReviewDb> MapReviewRequestToReviewDb(ReviewRequest reviewRequest)
        {
            return new ReviewDb()
            {
                Message = reviewRequest.Message
            };
        }
    }
}
