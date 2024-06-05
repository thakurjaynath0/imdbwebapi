
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class ActorMock
    {
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();
        public static readonly Mock<IMovieRepository> MovieRepositoryMock = new Mock<IMovieRepository>();   

        public static readonly List<ActorDb> ActorsList = MockList.GetActorsList();
        public static readonly List<GenderDb> GendersList = MockList.GetGendersList();
        public static readonly List<MovieDb> MoviesList = MockList.GetMoviesList(); 

        public static void MockAll()
        {
            // actor repository methods mock setup
            ActorRepositoryMock.Setup(a => a.GetActorsAsync())
                .ReturnsAsync(ActorsList.ToList());

            ActorRepositoryMock.Setup(a => a.GetActorAsync(It.IsAny<int>()))
                .ReturnsAsync((int actorId) => ActorsList.FirstOrDefault(actor => actor.Id == actorId));

            ActorRepositoryMock.Setup(a => a.CreateActorAsync(It.IsAny<ActorDb>()));

            ActorRepositoryMock.Setup(a => a.UpdateActorAsync(It.IsAny<ActorDb>()));

            ActorRepositoryMock.Setup(a => a.DeleteActorAsync(It.IsAny<int>()));

            // gender repository methods mock setup
            GenderRepositoryMock.Setup(g => g.GetGenderAsync(It.IsAny<int>()))
                .ReturnsAsync((int genderId) => GendersList.FirstOrDefault(gender => gender.Id == genderId));

            // movie repository methods mock setup
            MovieRepositoryMock.Setup(m => m.GetMoviesAsync())
                .ReturnsAsync(MoviesList.ToList());
        }
    }
}
