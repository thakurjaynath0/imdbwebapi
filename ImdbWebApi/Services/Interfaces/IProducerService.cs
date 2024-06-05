using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IProducerService
    {
        Task<IList<ProducerResponse>> GetProducersAsync();
        Task<ProducerResponse> GetProducerAsync(int producerId);
        Task CreateProducerAsync(ProducerRequest producer);
        Task UpdateProducerAsync(int producerId, ProducerRequest updatedProducer);
        Task DeleteProducerAsync(int producerId);
    }
}