﻿
namespace Wolny.P.Application.Services
{
    [Serializable]
    internal class UnexpectedException : Exception
    {
        public UnexpectedException()
        {
        }

        public UnexpectedException(string? message) : base(message)
        {
        }

        public UnexpectedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}