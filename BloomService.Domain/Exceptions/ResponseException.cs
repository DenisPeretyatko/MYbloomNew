namespace BloomService.Domain.Exceptions
{
    using System;

    using BloomService.Domain.Entities;

    public class ResponseException : Exception
    {
        public ResponseException(MessageResponsesMessageResponseError error)
        {
            Error = error;
        }

        public MessageResponsesMessageResponseError Error { get; }
    }
}