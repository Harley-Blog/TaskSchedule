namespace BC.TS.Exception
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}