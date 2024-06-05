using System.Collections.Generic;

namespace ImdbWebApi.Models.RequestModels
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorIds { get; set; }
        public List<int> GenreIds { get; set; }
        public string CoverImage { get; set; }
    }
}
