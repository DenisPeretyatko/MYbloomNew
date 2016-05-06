namespace BloomService.Web.Utils
{
    public class SageAuthorisationToken : IToken
    {
        public SageAuthorisationToken(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}