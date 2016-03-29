using Sage.WebApi.Infratructure.Service;
using System.Web.Mvc;

namespace Sage.WebApi.Areas.Api.Controllers
{
    [Authorize]
    public class AccountsReceivableController : BaseApiController
    {
        private readonly IServiceODBC _serviceODBCr;

        public AccountsReceivableController(IServiceODBC serviceODBCr)
        {
            _serviceODBCr = serviceODBCr;
        }
        public ActionResult Customers()
        {
            return Json(_serviceODBCr.Customers());
        }
    }
}