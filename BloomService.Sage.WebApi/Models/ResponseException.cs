using Sage.WebApi.Models.SerializeModels;
using System;

namespace Sage.WebApi.Models
{
    public class ResponseException : Exception
    {
        public MessageResponsesMessageResponseError Error { get; }
        public ResponseException(MessageResponsesMessageResponseError error)
        {
            Error = error;
        }
    }
}