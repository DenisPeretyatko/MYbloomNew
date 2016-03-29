using Sage.WebApi.Infratructure.Service;
using System.Web.Mvc;

namespace Sage.WebApi.Areas.Api.Controllers
{
    [Authorize]
    public class JobCostController : BaseApiController
    {
        private readonly IServiceODBC _serviceODBCr;

        public JobCostController(IServiceODBC serviceODBCr)
        {
            _serviceODBCr = serviceODBCr;
        }
        public ActionResult Trucks()
        {
            return Json(_serviceODBCr.Trucks(), JsonRequestBehavior.AllowGet);
        }
    }
}