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
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IModelMapper _mapper;

        public ProducerService(IProducerRepository producerRepository,
            IGenderRepository genderRepository,
            IMovieRepository movieRepository,
            IModelMapper mapper)
        {
            _producerRepository = producerRepository;
            _genderRepository = genderRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        private async Task ValidateProducer(ProducerRequest producerRequest)
        {
            // validate producer name
            StringValidator.Validate($"Producer name", producerRequest.Name).Required().MinLength(3).MaxLength(30);
            // validate producer dob
            DateTime validDob = DateTimeUtils.ParseFromYMD(producerRequest.DOB);
            DateValidator.Validate($"Producer DOB", validDob).MaxDate(DateTime.Now);
            // validate producer bio
            StringValidator.Validate($"Producer bio", producerRequest.Bio).Required().MinLength(10).MaxLength(50);
            // validate producer genderId
            var gender = await _genderRepository.GetGenderAsync(producerRequest.GenderId) ??
                throw new NotFoundException($"Gender with given id: {producerRequest.GenderId} not found.");
        }

        private async Task<ProducerDb> ValidateGetById(int producerId)
        {
            var producerDb = await _producerRepository.GetProducerAsync(producerId) ??
                throw new NotFoundException($"Producer with given id: {producerId} not found.");
            return producerDb;
        }

        private async Task ValidateDelete(int producerId)
        {
            var movies = await _movieRepository.GetMoviesAsync();
            var hasProducerProducedMovie = movies.Any(movie => movie.ProducerId == producerId);

            if (hasProducerProducedMovie)
            {
                throw new BadRequestException("Producer cannot be deleted.");
            }
        }

        public async Task CreateProducerAsync(ProducerRequest producerRequest)
        {
            await ValidateProducer(producerRequest);
            var newProducer = await _mapper.MapProducerRequestToProducerDb(producerRequest);
            await _producerRepository.CreateProducerAsync(newProducer);   
        }

        public async Task DeleteProducerAsync(int producerId)
        {
            await ValidateGetById(producerId);
            await ValidateDelete(producerId);
            await _producerRepository.DeleteProducerAsync(producerId);
        }

        public async Task<ProducerResponse> GetProducerAsync(int producerId)
        {
            var producerDb = await ValidateGetById(producerId);
            return await _mapper.MapProducerDbToProducerResponse(producerDb);
        }

        public async Task<IList<ProducerResponse>> GetProducersAsync()
        {
            var producers = await _producerRepository.GetProducersAsync(); 
            var list = new List<ProducerResponse>();
            
            foreach (var producer in producers)
            {
                list.Add(await _mapper.MapProducerDbToProducerResponse(producer));    
            }

            return list;
        }

        public async Task UpdateProducerAsync(int producerId, ProducerRequest updatedProducer)
        {
            var producerDb = await ValidateGetById(producerId);
            await ValidateProducer(updatedProducer);
            producerDb = await _mapper.MapProducerRequestToProducerDb(updatedProducer);
            producerDb.Id = producerId;
            await _producerRepository.UpdateProducerAsync(producerDb);
        }
    }
}
