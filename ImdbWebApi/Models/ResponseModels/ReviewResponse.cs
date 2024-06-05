namespace ImdbWebApi.Models.ResponseModels
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
