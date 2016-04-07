namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Web.Mvc;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Exceptions;
    using BloomService.Web.Managers;

    [Authorize]
    public class BloomServiceManagementController : BaseApiController
    {
        private readonly ISageApiManager sageApiManager;

        public BloomServiceManagementController(ISageApiManager sageApiManager)
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
        [AllowAnonymous]
        public ActionResult Repairs()
        {
            return Json(sageApiManager.Repairs());
        }

        [AllowAnonymous]
        public ActionResult Employees()
        {
            return Json(sageApiManager.Employees());
        }

        [AllowAnonymous]
        public ActionResult Workorders(string id)
        {
            return id == null ? Json(sageApiManager.Workorders()) : Json(sageApiManager.Workorders(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Workorders(Properties properties)
        {
            try
            {
                return Json(sageApiManager.Workorders(properties));
            }
            catch (ResponseException ex)
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
            return Json(sageApiManager.Equipment());
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
    }
}