namespace Sage.WebApi.Areas.Api.Controllers
{
    using System.Web.Mvc;

    using Sage.WebApi.Infratructure.Service;

    [Authorize]
    public class AccountsReceivableController : BaseApiController
    {
        private readonly IServiceOdbc serviceOdbc;

        public AccountsReceivableController(IServiceOdbc serviceOdbc)
        {
            this.serviceOdbc = serviceOdbc;
        }

        public ActionResult Customers()
        {
            return Json(serviceOdbc.Customers());
        }
    }
}