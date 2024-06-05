using ImdbWebApi.Exceptions;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils;
using ImdbWebApi.Utils.ModelMappers;
using ImdbWebApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IModelMapper _mapper;

        public ActorService(IActorRepository actorRepository,
            IGenderRepository genderRepository,
            IMovieRepository movieRepository,
            IModelMapper mapper)
        {
            _actorRepository = actorRepository;
            _genderRepository = genderRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        private async Task ValidateActor(ActorRequest actorRequest)
        {
            // validate actor name
            StringValidator.Validate($"Actor name", actorRequest.Name).Required().MinLength(3).MaxLength(30);
            // validate actor dob
            DateTime validDob = DateTimeUtils.ParseFromYMD(actorRequest.DOB);
            DateValidator.Validate($"Actor DOB", validDob).MaxDate(DateTime.Now);
            // validate actor bio
            StringValidator.Validate($"Actor bio", actorRequest.Bio).Required().MinLength(10).MaxLength(50);
            // validate actor genderId
            var gender = await _genderRepository.GetGenderAsync(actorRequest.GenderId) ??
                throw new NotFoundException($"Gender with given id: {actorRequest.GenderId} not found.");
        }

        private async Task<ActorDb> ValidateGetById(int actorId)
        {
            var actorDb = await _actorRepository.GetActorAsync(actorId) ??
                throw new NotFoundException($"Actor with given id: {actorId} not found.");
            return actorDb;
        }

        private async Task ValidateDelete(int actorId)
        {
            var movies = await _movieRepository.GetMoviesAsync();
            var hasActorPlayedMovie = movies.Any(movie =>
            {
                return movie.ActorIds.Split(',').Select(actorId => Convert.ToInt32(actorId)).Any(actor => actor == actorId);
            });

            if (hasActorPlayedMovie)
            {
                throw new BadRequestException("Actor cannot be deleted.");
            }
        }

        public async Task CreateActorAsync(ActorRequest actorRequest)
        {
            await ValidateActor(actorRequest);
            var actorDb = await _mapper.MapActorRequestToActorDb(actorRequest);
            await _actorRepository.CreateActorAsync(actorDb);
        }

        public async Task DeleteActorAsync(int actorId)
        {
            await ValidateGetById(actorId);
            await ValidateDelete(actorId);
            await _actorRepository.DeleteActorAsync(actorId);
        }

        public async Task<ActorResponse> GetActorAsync(int actorId)
        {
            var actorDb = await ValidateGetById(actorId);
            return await _mapper.MapActorDbToActorResponse(actorDb);
        }

        public async Task<IList<ActorResponse>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetActorsAsync();
            var list = new List<ActorResponse>();

            foreach (var actor in actors)
            {
                list.Add(await _mapper.MapActorDbToActorResponse(actor));
            }

            return list;
        }

        public async Task UpdateActorAsync(int actorId, ActorRequest updatedActor)
        {
            var actorDb = await ValidateGetById(actorId);
            await ValidateActor(updatedActor);
            actorDb = await _mapper.MapActorRequestToActorDb(updatedActor);
            actorDb.Id = actorId;
            await _actorRepository.UpdateActorAsync(actorDb);    
        }
    }
}
