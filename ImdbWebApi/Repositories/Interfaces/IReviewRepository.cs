using ImdbWebApi.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IList<ReviewDb>> GetReviewsAsync(int movieId);
        Task<ReviewDb> GetReviewAsync(int movieId, int reviewId);
        Task<ReviewDb> GetReviewByIdAsync(int reviewId);
        Task CreateReviewAsync(ReviewDb reviewDb);
        Task UpdateReviewAsync(ReviewDb updatedReview);
        Task DeleteReviewAsync(int reviewId);
    }
}
