
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IMovieRepository> MovieRepositoryMock = new Mock<IMovieRepository>();

        public static readonly List<ProducerDb> ProducersList = MockList.GetProducersList();
        public static readonly List<GenderDb> GendersList = MockList.GetGendersList();
        public static readonly List<MovieDb> MoviesList = MockList.GetMoviesList();
        
        public static void MockAll()
        {
            // producer repository methods mock setup
            ProducerRepositoryMock.Setup(p => p.GetProducersAsync())
                .ReturnsAsync(ProducersList.ToList());

            ProducerRepositoryMock.Setup(p => p.GetProducerAsync(It.IsAny<int>()))
                .ReturnsAsync((int producerId) => ProducersList.FirstOrDefault(producer => producer.Id == producerId));

            ProducerRepositoryMock.Setup(p => p.CreateProducerAsync(It.IsAny<ProducerDb>()));

            ProducerRepositoryMock.Setup(p => p.UpdateProducerAsync(It.IsAny<ProducerDb>()));

            ProducerRepositoryMock.Setup(p => p.DeleteProducerAsync(It.IsAny<int>()));

            // gender repository methods mock setup
            GenderRepositoryMock.Setup(g => g.GetGenderAsync(It.IsAny<int>()))
                .ReturnsAsync((int genderId) => GendersList.FirstOrDefault(gender => gender.Id == genderId));

            // movie repository methods mock setup
            MovieRepositoryMock.Setup(m => m.GetMoviesAsync())
                .ReturnsAsync(MoviesList.ToList());
        }
    }
}
