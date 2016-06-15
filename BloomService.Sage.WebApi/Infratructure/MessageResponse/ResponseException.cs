namespace Sage.WebApi.Infratructure.MessageResponse
{
    using System;

    public class ResponseException : Exception
    {
        public ResponseException(ResponseError error)
        {
            Error = error;
        }

        public ResponseError Error { get; set; }
    }
}