namespace BloomService.Domain.Models.Responses
{
    public class AuthorizationResponse
    {
        public long Id;
        public AuthorizationType Type;
        public string Mail;
        public string Token;
    }

    public enum AuthorizationType
    {
        Technician = 1,
        Manager = 2
    }
}
