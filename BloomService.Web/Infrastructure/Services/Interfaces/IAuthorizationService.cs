using System.Security.Claims;
using BloomService.Domain.Models.Responses;
using BloomService.Web.Models;

namespace BloomService.Web.Services.Abstract
{
    public interface IAuthorizationService
    {
        UserModel GetUser(ClaimsPrincipal claimsPrincipal);
        AuthorizationResponse CheckUser(string name, string password);
        string GetAuthToken();
    }
}