namespace BloomService.Domain.Exceptions
{
    using System;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.MessageResponse;

    public class ResponseException : Exception
    {
        public ResponseException(ResponseError error)
        {
            Error = error;
        }

        public ResponseError Error { get; set; }
    }
}