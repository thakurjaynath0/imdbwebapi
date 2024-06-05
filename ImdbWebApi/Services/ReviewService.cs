using ImdbWebApi.Exceptions;
using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Models.RequestModels;
using ImdbWebApi.Models.ResponseModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils.ModelMappers;
using ImdbWebApi.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbWebApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IModelMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository,
            IMovieRepository movieRepository,
            IModelMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        private void ValidateReview(ReviewRequest reviewRequest)
        {
            // validate review message
            StringValidator.Validate("Review message", reviewRequest.Message).Required().MinLength(10).MaxLength(500);

        }

        private async Task ValidateMovieId(int movieId)
        {
            var movieDb = await _movieRepository.GetMovieAsync(movieId) ??
                throw new NotFoundException($"Movie with given id: {movieId} not found.");
        }

        private async Task<ReviewDb> ValidateGetById(int reviewId)
        {
            var reviewDb = await _reviewRepository.GetReviewByIdAsync(reviewId) ??
                throw new NotFoundException($"Review with given id: {reviewId} not found.");
            return reviewDb;
        }

        private async Task<ReviewDb> ValidateGetReview(int movieId, int reviewId)
        {
            var reviewDb = await _reviewRepository.GetReviewAsync(movieId, reviewId) ??
                throw new NotFoundException($"Review with given id: {reviewId} not found.");
            return reviewDb;
        }

        public async Task CreateReviewAsync(int movieId, ReviewRequest reviewRequest)
        {
            await ValidateMovieId(movieId);
            ValidateReview(reviewRequest);
            var newReview = await _mapper.MapReviewRequestToReviewDb(reviewRequest);
            newReview.MovieId = movieId;
            await _reviewRepository.CreateReviewAsync(newReview); 
        }

        public async Task DeleteReviewAsync(int movieId, int reviewId)
        {
            await ValidateMovieId(movieId);
            await ValidateGetReview(movieId, reviewId);
            await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<ReviewResponse> GetReviewAsync(int movieId, int reviewId)
        {
            await ValidateMovieId(movieId);
            var reviewDb = await ValidateGetReview(movieId, reviewId);
            return await _mapper.MapReviewDbToReviewResponse(reviewDb);
        }

        public async Task<ReviewResponse> GetReviewByIdAsync(int reviewId)
        {
            var reviewDb = await ValidateGetById(reviewId);
            return await _mapper.MapReviewDbToReviewResponse(reviewDb);
        }

        public async Task<IList<ReviewResponse>> GetReviewsAsync(int movieId)
        {
            await ValidateMovieId(movieId);
            var reviews = await _reviewRepository.GetReviewsAsync(movieId);
            var list = new List<ReviewResponse>();  
            
            foreach (var review in reviews)
            {
                list.Add(await _mapper.MapReviewDbToReviewResponse(review));
            }

            return list;
        }

        public async Task UpdateReviewAsync(int movieId, int reviewId, ReviewRequest updatedReview)
        {
            await ValidateMovieId(movieId);
            var reviewDb = await ValidateGetReview(movieId, reviewId);
            ValidateReview(updatedReview);
            reviewDb = await _mapper.MapReviewRequestToReviewDb(updatedReview);
            reviewDb.Id = reviewId;
            reviewDb.MovieId = movieId;
            await _reviewRepository.UpdateReviewAsync(reviewDb);
        }
    }
}
