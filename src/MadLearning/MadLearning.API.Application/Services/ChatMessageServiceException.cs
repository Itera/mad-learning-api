using System;

namespace MadLearning.API.Application.Services
{
    public sealed class ChatMessageServiceException : Exception
    {
        public ChatMessageServiceException(string message)
            : base(message)
        {
        }

        public ChatMessageServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
