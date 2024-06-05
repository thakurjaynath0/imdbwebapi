
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class GenderMock
    {
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();

        public static readonly List<GenderDb> GendersList = MockList.GetGendersList();
        public static readonly List<ActorDb> ActorsList = MockList.GetActorsList();
        public static readonly List<ProducerDb> ProducersList = MockList.GetProducersList();


        public static void MockAll()
        {
            // gender repository methods mock setup
            GenderRepositoryMock.Setup(g => g.GetGendersAsync())
                .ReturnsAsync(GendersList.ToList());

            GenderRepositoryMock.Setup(g => g.GetGenderAsync(It.IsAny<int>()))
                .ReturnsAsync((int genderId) => GendersList.FirstOrDefault(gender => gender.Id == genderId));

            GenderRepositoryMock.Setup(g => g.CreateGenderAsync(It.IsAny<GenderDb>()));

            GenderRepositoryMock.Setup(g => g.UpdateGenderAsync(It.IsAny<GenderDb>()));

            GenderRepositoryMock.Setup(g => g.DeleteGenderAsync(It.IsAny<int>()));

            // actor repository methods mock setup
            ActorRepositoryMock.Setup(a => a.GetActorsAsync())
                .ReturnsAsync(ActorsList.ToList());

            // producer repository methods mock setup
            ProducerRepositoryMock.Setup(p => p.GetProducersAsync())
                .ReturnsAsync(ProducersList.ToList());
        }
    }
}
 