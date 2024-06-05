using System.Collections.Generic;

namespace ImdbWebApi.Models.DbModels
{
    public class MovieDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public int ProducerId { get; set; }
        public string ActorIds { get; set; }
        public string GenreIds { get; set; }
        public string CoverImage { get; set; }
    }
}
