
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IMovieRepository> MovieRepositoryMock = new Mock<IMovieRepository>();
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();

        public static readonly List<GenreDb> GenresList = MockList.GetGenresList();
        public static readonly List<MovieDb> MoviesList = MockList.GetMoviesList(); 

        public static void MockAll()
        {
            // genre repository methods mock setup
            GenreRepositoryMock.Setup(g => g.GetGenresAsync())
                .ReturnsAsync(GenresList.ToList());

            GenreRepositoryMock.Setup(g => g.GetGenreAsync(It.IsAny<int>()))
               .ReturnsAsync((int genreId) => GenresList.FirstOrDefault(genre => genre.Id == genreId));

            GenreRepositoryMock.Setup(g => g.CreateGenreAsync(It.IsAny<GenreDb>()));

            GenreRepositoryMock.Setup(g => g.UpdateGenreAsync(It.IsAny<GenreDb>()));

            GenreRepositoryMock.Setup(g => g.DeleteGenreAsync(It.IsAny<int>()));

            // movie repository methods mock setup
            MovieRepositoryMock.Setup(m => m.GetMoviesAsync())
                .ReturnsAsync(MoviesList.ToList());
        }
    }
}
