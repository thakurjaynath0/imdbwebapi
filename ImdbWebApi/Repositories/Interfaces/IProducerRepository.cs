using ImdbWebApi.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        Task<IList<ProducerDb>> GetProducersAsync();
        Task<ProducerDb> GetProducerAsync(int producerId);
        Task CreateProducerAsync(ProducerDb producerDb);
        Task UpdateProducerAsync(ProducerDb updatedProducer);
        Task DeleteProducerAsync(int producerId);
    }
}
