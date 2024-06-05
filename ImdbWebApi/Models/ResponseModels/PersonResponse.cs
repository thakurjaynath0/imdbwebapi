namespace ImdbWebApi.Models.ResponseModels
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Bio { get; set; }
        public GenderResponse Gender { get; set; }
    }
}
