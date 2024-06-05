using ImdbWebApi.Models.DbModels;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApiComplete;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWebApi.Repositories
{
    public class ReviewRepository : BaseRepository<ReviewDb>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDBDB)
        {
            
        }

        public async Task CreateReviewAsync(ReviewDb reviewDb)
        {
            const string query = @"
            INSERT INTO Reviews (
	            MovieId
	            ,Message
	            )
            VALUES (
	            @MovieId
	            ,@Message
	            )";

            await ExecuteQueryAsync(query, reviewDb);
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            const string query = @"
            DELETE
            FROM Reviews
            WHERE [Id] = @Id";

            await ExecuteQueryAsync(query, new { Id = reviewId });
        }

        public async Task<ReviewDb> GetReviewAsync(int movieId, int reviewId)
        {
            const string query = @"
            SELECT [Id]
                , [MovieId]
	            , [Message]
            FROM Reviews
            WHERE [Id] = @ReviewId
                AND [MovieId] = @MovieId";

            return await GetAsync(query, new { ReviewId = reviewId, MovieId = movieId });
        }

        public async Task<ReviewDb> GetReviewByIdAsync(int reviewId)
        {
            const string query = @"
            SELECT [Id]
                , [MovieId]
	            , [Message]
            FROM Reviews
            WHERE [Id] = @ReviewId";

            return await GetAsync(query, new { ReviewId = reviewId });
        }


        public async Task<IList<ReviewDb>> GetReviewsAsync(int movieId)
        {
            const string query = @"
            SELECT [Id]
                , [MovieId]
	            , [Message]
            FROM Reviews
            WHERE [MovieId] = @MovieId";

            var reviews = await GetAllAsync(query, new { MovieId = movieId });
            return reviews.ToList();
        }

        public async Task UpdateReviewAsync(ReviewDb updatedReview)
        {
            const string query = @"
            UPDATE Reviews
            SET Message = @Message
            WHERE [Id] = @Id
	            AND [MovieId] = @MovieId";

            await ExecuteQueryAsync(query, updatedReview);
        }
    }
}
