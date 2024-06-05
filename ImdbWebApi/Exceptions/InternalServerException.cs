namespace ImdbWebApi.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message)
            : base(500, message)
        {
            
        }
    }
}
