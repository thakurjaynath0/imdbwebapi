using ImdbWebApi.Exceptions;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils.ModelMappers;
using ImdbWebApi.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IModelMapper _mapper;

        public GenderService(IGenderRepository genderRepository,
            IActorRepository actorRepository,
            IProducerRepository producerRepository,
            IModelMapper mapper)
        {
            _genderRepository = genderRepository;
            _actorRepository = actorRepository;
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        private void ValidateGender(GenderRequest genderRequest)
        {
            // validate gender name
            StringValidator.Validate("Gender", genderRequest.Name).Required().MinLength(3).MaxLength(30);
        }

        private async Task<GenderDb> ValidateGetById(int genderId)
        {
            var genderDb = await _genderRepository.GetGenderAsync(genderId) ??
                throw new NotFoundException($"Gender with given id: {genderId} not found.");
            return genderDb;
        }

        private async Task ValidateDelete(int genderId)
        {
            var actors = await _actorRepository.GetActorsAsync();
            var hasActorGivenGender = actors.Any(actor => actor.GenderId == genderId);

            if (hasActorGivenGender)
            {
                throw new BadRequestException("Gender cannot be deleted.");
            }

            var producers = await _producerRepository.GetProducersAsync();  
            var hasProducerGivenGender = producers.Any(producer => producer.GenderId == genderId);

            if (hasProducerGivenGender)
            {
                throw new BadRequestException("Gender cannot be deleted.");
            }
        }

        public async Task CreateGenderAsync(GenderRequest genderRequest)
        {
            ValidateGender(genderRequest);
            var newGender = await _mapper.MapGenderRequestToGenderDb(genderRequest);
            await _genderRepository.CreateGenderAsync(newGender);  
        }

        public async Task DeleteGenderAsync(int genderId)
        {
            await ValidateGetById(genderId);    
            await ValidateDelete(genderId); 
            await _genderRepository.DeleteGenderAsync(genderId);
        }

        public async Task<GenderResponse> GetGenderAsync(int genderId)
        {
            var genderDb = await ValidateGetById(genderId);
            return await _mapper.MapGenderDbToGenderResponse(genderDb);
        }

        public async Task<IList<GenderResponse>> GetGendersAsync()
        {
            var genders = await _genderRepository.GetGendersAsync();
            var list = new List<GenderResponse>();
            
            foreach (var gender in genders)
            {
                list.Add(await _mapper.MapGenderDbToGenderResponse(gender));
            }

            return list;
        }

        public async Task UpdateGenderAsync(int genderId, GenderRequest updatedGender)
        {
            var genderDb = await ValidateGetById(genderId);
            ValidateGender(updatedGender);  
            genderDb = await _mapper.MapGenderRequestToGenderDb(updatedGender);
            genderDb.Id = genderId;
            await _genderRepository.UpdateGenderAsync(genderDb);
        }
    }
}
