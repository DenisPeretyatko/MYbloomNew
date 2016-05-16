using System.Web.Mvc;
using Sage.WebApi.Infratructure.Service;
using BloomService.Domain.Models.Requests;

namespace Sage.WebApi.Areas.Api.Controllers
{
    [Authorize]
    public class AuthorizationController : BaseApiController
    {
        private readonly IServiceAuthorization _serviceAuthorization;

        public AuthorizationController(IServiceAuthorization serviceAuthorization)
        {
            _serviceAuthorization = serviceAuthorization;
        }
        public ActionResult Authorization(AuthorizationRequest model)
        {
            var response = _serviceAuthorization.Authorization(model);
            return Json(response);
        }
    }
}
