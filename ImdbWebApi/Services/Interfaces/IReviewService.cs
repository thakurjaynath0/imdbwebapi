using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IList<ReviewResponse>> GetReviewsAsync(int movieId);
        Task<ReviewResponse> GetReviewAsync(int movieId, int reviewId);
        Task<ReviewResponse> GetReviewByIdAsync(int reviewId);
        Task CreateReviewAsync(int movieId, ReviewRequest review);
        Task UpdateReviewAsync(int movieId, int reviewId, ReviewRequest updatedReview);
        Task DeleteReviewAsync(int movieId, int reviewId);
    }
}