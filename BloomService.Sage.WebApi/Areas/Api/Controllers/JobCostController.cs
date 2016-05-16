using System.Web.Mvc;

using Sage.WebApi.Infratructure.Service;

namespace Sage.WebApi.Areas.Api.Controllers
{
    [Authorize]
    public class JobCostController : BaseApiController
    {
        private readonly IServiceOdbc _serviceODBCr;

        public JobCostController(IServiceOdbc serviceODBCr)
        {
            _serviceODBCr = serviceODBCr;
        }

        public ActionResult Trucks()
        {
            return Json(_serviceODBCr.Trucks(), JsonRequestBehavior.AllowGet);
        }
    }
}