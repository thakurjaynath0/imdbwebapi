using System.Collections.Generic;
using System.Threading.Tasks;
using ImdbWebApi.Models.DbModels;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IActorRepository
    {
        Task<IList<ActorDb>> GetActorsAsync();
        Task<ActorDb> GetActorAsync(int actorId);
        Task CreateActorAsync(ActorDb actorDb);
        Task UpdateActorAsync(ActorDb updatedActor);
        Task DeleteActorAsync(int actorId);
    }
}
