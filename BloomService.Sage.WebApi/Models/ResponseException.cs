using System;

namespace Sage.WebApi.Models
{
    using BloomService.Domain.Entities;

    public class ResponseException : Exception
    {
        public MessageResponsesMessageResponseError Error { get; }
        public ResponseException(MessageResponsesMessageResponseError error)
        {
            Error = error;
        }
    }
}