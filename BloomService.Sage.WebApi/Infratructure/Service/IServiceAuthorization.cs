using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;

namespace Sage.WebApi.Infratructure.Service
{
    public interface IServiceAuthorization
    {
        AuthorizationResponse Authorization(AuthorizationRequest model);
    }
}
