namespace ImdbWebApi.Models.DbModels
{
    public class ReviewDb
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
