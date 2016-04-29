namespace BloomService.Domain.Exceptions
{
    using System;

    using BloomService.Domain.Entities.Concrete.MessageResponse;

    public class ResponseException : Exception
    {
        public ResponseException(ResponseError error)
        {
            Error = error;
        }

        public ResponseError Error { get; set; }
    }
}