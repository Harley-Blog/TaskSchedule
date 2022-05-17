using System;

namespace BC.TS.Exception
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}