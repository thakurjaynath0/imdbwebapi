
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImdbWebApi.Test.MockResources
{
    public class ReviewMock
    {
        public static readonly Mock<IReviewRepository> ReviewRepositoryMock = new Mock<IReviewRepository>();
        public static readonly Mock<IMovieRepository> MovieRepsitoryMock = new Mock<IMovieRepository>();
        public static readonly Mock<IActorRepository> ActorRepositoryMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenderRepository> GenderRepositoryMock = new Mock<IGenderRepository>();
        public static readonly Mock<IGenreRepository> GenreRepositoryMock = new Mock<IGenreRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepositoryMock = new Mock<IProducerRepository>();

        public static readonly List<ReviewDb> ReviewsList = MockList.GetReviewsList();
        public static readonly List<MovieDb> MoviesList = MockList.GetMoviesList(); 

        public static void MockAll()
        {
            // review repository methods mock setup
            ReviewRepositoryMock.Setup(r => r.GetReviewsAsync(It.IsAny<int>()))
                .ReturnsAsync((int movieId) => ReviewsList.Where(review => review.MovieId ==  movieId).ToList());

            ReviewRepositoryMock.Setup(r => r.GetReviewAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int movieId, int reviewId) => ReviewsList.FirstOrDefault(review => review.Id == reviewId && review.MovieId == movieId));

            ReviewRepositoryMock.Setup(r => r.CreateReviewAsync(It.IsAny<ReviewDb>()));

            ReviewRepositoryMock.Setup(r => r.UpdateReviewAsync(It.IsAny<ReviewDb>()));

            ReviewRepositoryMock.Setup(r => r.DeleteReviewAsync(It.IsAny<int>()));

            // movie repository methods mock setup
            MovieRepsitoryMock.Setup(m => m.GetMovieAsync(It.IsAny<int>()))
                .ReturnsAsync((int movieId) => MoviesList.FirstOrDefault(movie => movie.Id == movieId));    
        }
    }
}
