using System.Threading.Tasks;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;

namespace ImdbWebApi.Utils.ModelMappers
{
    public interface IModelMapper
    {
        Task<ActorDb> MapActorRequestToActorDb(ActorRequest actorRequest);
        Task<ActorResponse> MapActorDbToActorResponse(ActorDb actorDb);
        Task<ProducerDb> MapProducerRequestToProducerDb(ProducerRequest producerRequest);
        Task<ProducerResponse> MapProducerDbToProducerResponse(ProducerDb producerDb);
        Task<GenderDb> MapGenderRequestToGenderDb(GenderRequest genderRequest);   
        Task<GenderResponse> MapGenderDbToGenderResponse(GenderDb genderDb);
        Task<GenreDb> MapGenreRequestToGenreDb(GenreRequest genreRequest);
        Task<GenreResponse> MapGenreDbToGenreResponse(GenreDb genreDb);
        Task<MovieDb> MapMovieRequestToMovieDb(MovieRequest movieRequest);
        Task<MovieResponse> MapMovieDbToMovieResponse(MovieDb movieDb);
        Task<ReviewDb> MapReviewRequestToReviewDb(ReviewRequest reviewRequest);
        Task<ReviewResponse> MapReviewDbToReviewResponse(ReviewDb reviewDb);
    }
}
