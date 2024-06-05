namespace ImdbWebApi.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(404, message)
        {
            
        }
    }
}
