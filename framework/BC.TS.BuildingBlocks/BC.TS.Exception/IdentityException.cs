using System.Net;

namespace BC.TS.Exception;

public class IdentityException: CustomException
{
    public IdentityException(string message = default, HttpStatusCode statusCode = default)
        : base(message, statusCode)
    {
    }
}