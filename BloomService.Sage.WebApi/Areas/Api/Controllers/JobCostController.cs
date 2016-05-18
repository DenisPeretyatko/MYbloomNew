namespace Sage.WebApi.Areas.Api.Controllers
{
    using System.Web.Mvc;

    using Sage.WebApi.Infratructure.Service;

    [Authorize]
    public class JobCostController : BaseApiController
    {
        private readonly IServiceOdbc serviceOdbc;

        public JobCostController(IServiceOdbc serviceOdbc)
        {
            this.serviceOdbc = serviceOdbc;
        }

        public ActionResult Trucks()
        {
            return Json(serviceOdbc.Trucks(), JsonRequestBehavior.AllowGet);
        }
    }
}