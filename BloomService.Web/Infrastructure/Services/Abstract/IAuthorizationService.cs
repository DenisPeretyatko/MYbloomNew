using BloomService.Domain.Models.Responses;
using BloomService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BloomService.Web.Services.Abstract
{
    public interface IAuthorizationService
    {
        UserModel GetUser(ClaimsPrincipal claimsPrincipal);
        IEnumerable<Claim> SetClaims(string name, string pass, string id, string type, string mail);
        Claim CreateClaim(string type, string value);
        AuthorizationResponse CheckUser(string name, string password);
        string GetAuthToken();
    }
}