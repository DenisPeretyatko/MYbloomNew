namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using System.Security.Claims;

    using BloomService.Domain.Models.Responses;
    using BloomService.Web.Models;

    public interface IAuthorizationService
    {
        UserModel GetUser(ClaimsPrincipal claimsPrincipal);
        //AuthorizationResponse CheckUser(string name, string password);
        AuthorizationResponse Authorization(string login, string password);
    }
}