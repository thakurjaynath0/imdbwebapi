
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class MovieMock
    {
        public static readonly Mock<IMovieRepository> MovieRepositoryMock = new Mock<IMovieRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();  
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IReviewRepository> ReviewRepositoryMock = new Mock<IReviewRepository>();
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();    


        public static readonly List<MovieDb> MoviesList = MockList.GetMoviesList();
        public static readonly List<ProducerDb> ProducersList = MockList.GetProducersList();
        public static readonly List<ActorDb> ActorsList = MockList.GetActorsList();
        public static readonly List<GenreDb> GenresList = MockList.GetGenresList();
        public static readonly List<ReviewDb> ReviewsList = MockList.GetReviewsList();  
        public static readonly List<GenderDb> GendersList = MockList.GetGendersList();

        public static void MockAll()
        {
            // movie repository methods mock setup
            MovieRepositoryMock.Setup(m => m.GetMoviesGivenYearAsync(It.IsAny<int>()))
                .ReturnsAsync((int year) => MoviesList.Where(movie => movie.YearOfRelease == year).ToList());

            MovieRepositoryMock.Setup(m => m.GetMovieAsync(It.IsAny<int>()))
               .ReturnsAsync((int movieId) => MoviesList.FirstOrDefault(movie => movie.Id == movieId));

            MovieRepositoryMock.Setup(m => m.CreateMovieAsync(It.IsAny<MovieDb>()));

            MovieRepositoryMock.Setup(m => m.UpdateMovieAsync(It.IsAny<MovieDb>()));

            MovieRepositoryMock.Setup(m => m.DeleteMovieAsync(It.IsAny<int>()));

            // producer repository methods mock setup
            ProducerRepositoryMock.Setup(p => p.GetProducerAsync(It.IsAny<int>()))
                .ReturnsAsync((int producerId) => ProducersList.FirstOrDefault(producer => producer.Id == producerId));

            // actor repository methods mock setup
            ActorRepositoryMock.Setup(a => a.GetActorAsync(It.IsAny<int>()))
                .ReturnsAsync((int actorId) => ActorsList.FirstOrDefault(actor => actor.Id == actorId));

            // gender repository methods mock setup
            GenderRepositoryMock.Setup(g => g.GetGenderAsync(It.IsAny<int>()))
                .ReturnsAsync((int genderId) => GendersList.FirstOrDefault(gender => gender.Id == genderId));

            // genre repository methods mock setup
            GenreRepositoryMock.Setup(g => g.GetGenreAsync(It.IsAny<int>()))
               .ReturnsAsync((int genreId) => GenresList.FirstOrDefault(genre => genre.Id == genreId));

            // review repository methods mock setup
            ReviewRepositoryMock.Setup(r => r.GetReviewsAsync(It.IsAny<int>()))
               .ReturnsAsync((int movieId) => ReviewsList.Where(review => review.MovieId == movieId).ToList());
        }
    }
}
