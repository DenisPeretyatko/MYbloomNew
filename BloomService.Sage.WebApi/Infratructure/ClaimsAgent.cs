namespace Sage.WebApi.Infratructure
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;

    public class ClaimsAgent
    {
        public string Name;
        public string Password;
        public ClaimsAgent()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            Password = identity.Claims.Where(c => c.Type == ClaimTypes.Surname)
                               .Select(c => c.Value).SingleOrDefault();
        }
    }
}