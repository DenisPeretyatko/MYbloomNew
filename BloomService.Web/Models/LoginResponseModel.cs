namespace BloomService.Web.Models
{
    public class LoginResponseModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string Id { get; set; }
    }
}