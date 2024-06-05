using System;

namespace ImdbWebApi.Models.DbModels
{
    public class PersonDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public int GenderId { get; set; }
    }
}
