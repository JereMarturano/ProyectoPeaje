using System;

namespace TollSystem.Domain.Exceptions
{
    public class InvalidLicensePlateFormatException : Exception
    {
        public InvalidLicensePlateFormatException(string message) : base(message)
        {
        }
    }
}
