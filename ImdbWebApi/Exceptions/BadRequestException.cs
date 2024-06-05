namespace ImdbWebApi.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(400, message)
        {
            
        }
    }
}
