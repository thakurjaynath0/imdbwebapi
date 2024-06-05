using System;

namespace ImdbWebApi.Models.RequestModels
{
    public class PersonRequest
    {
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Bio { get; set; }
        public int GenderId { get; set; }
    }
}
