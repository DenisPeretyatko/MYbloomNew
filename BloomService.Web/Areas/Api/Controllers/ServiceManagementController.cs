namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Web.Mvc;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Exceptions;
    using BloomService.Web.Managers;

    [Authorize]
    public class ServiceManagementController : BaseApiController
    {
        private readonly ISageApiManager sageApiManager;

        public ServiceManagementController(ISageApiManager sageApiManager)
        {
            this.sageApiManager = sageApiManager;
        }
        
        public ActionResult Locations()
        {
            return Json(sageApiManager.Locations());
        }

        public ActionResult Parts()
        {
            return Json(sageApiManager.Parts());
        }

        public ActionResult Problems()
        {
            return Json(sageApiManager.Problems());
        }

        public ActionResult Repairs()
        {
            return Json(sageApiManager.Repairs());
        }

        [AllowAnonymous]
        public ActionResult Employees()
        {
            return Json(sageApiManager.Employees());
        }
        
        public ActionResult Workorders(string id)
        {
            return id == null ? Json(sageApiManager.WorkOrders()) : Json(sageApiManager.WorkOrders(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Workorders(Properties properties)
        {
            try
            {
                return Json(sageApiManager.WorkOrders(properties));
            }
            catch(ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        public ActionResult Calltypes()
        {
            return Json(sageApiManager.Calltypes());
        }

        public ActionResult Departments()
        {
            return Json(sageApiManager.Departments());
        }

        public ActionResult Equipment()
        {
            return Json(sageApiManager.Equipments());
        }

        public ActionResult Assignments(string id)
        {
            return id == null ? Json(sageApiManager.Assignments()) : Json(sageApiManager.Assignments(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAssignments(Properties properties)
        {
            try
            {
                return Json(sageApiManager.AddAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAssignments(Properties properties)
        {
            try
            {
                return Json(sageApiManager.EditAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        public ActionResult RateSheets()
        {
            return Json(sageApiManager.RateSheet());
        }

        public ActionResult PermissionCodes()
        {
            return Json(sageApiManager.PermissionCode());
        }

        public ActionResult Agreements()
        {
            return Json(sageApiManager.Agreements());
        }
    }
}