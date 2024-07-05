namespace Wolny.P.Application.Exceptions
{
    [Serializable]
    internal class InvalidException : Exception
    {
        public InvalidException()
        {
        }

        public InvalidException(string? message) : base(message)
        {
        }

        public InvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}