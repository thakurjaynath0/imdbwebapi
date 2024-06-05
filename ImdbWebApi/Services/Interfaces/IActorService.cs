using System.Collections.Generic;
using System.Threading.Tasks;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IActorService
    {
        Task<IList<ActorResponse>> GetActorsAsync();
        Task<ActorResponse> GetActorAsync(int actorId);
        Task CreateActorAsync(ActorRequest actor);   
        Task UpdateActorAsync(int actorId, ActorRequest updatedActor);
        Task DeleteActorAsync(int actorId);
    }
}