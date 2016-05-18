namespace Sage.WebApi.Areas.Api.Controllers
{
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Exceptions;

    using Sage.WebApi.Infratructure.Service;

    [Authorize]
    public class ServiceManagementController : BaseApiController
    {
        private readonly IServiceManagement serviceManager;
        private readonly IServiceOdbc serviceOdbc;

        public ServiceManagementController(IServiceManagement serviceManager, IServiceOdbc serviceOdbc)
        {
            this.serviceManager = serviceManager;
            this.serviceOdbc = serviceOdbc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAssignments(SagePropertyDictionary properties)
        {
            try
            {
                return Json(serviceManager.AddAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        public ActionResult Agreements()
        {
            return Json(serviceManager.Agreements());
        }

        [GET("api/v1/sm/assignments/{id}")]
        public ActionResult Assignments(string id)
        {
            return id == null ? Json(serviceManager.Assignments()) : Json(serviceManager.Assignments(id));
        }

        [HttpDelete]
        public ActionResult UnassignWorkOrder(string id)
        {
            serviceOdbc.UnassignWorkOrder(id);
            return Json(serviceManager.Assignments());
        }

        public ActionResult Calltypes()
        {
            return Json(serviceManager.Calltypes());
        }

        public ActionResult Departments()
        {
            return Json(serviceManager.Departments());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAssignments(SagePropertyDictionary properties)
        {
            try
            {
                return Json(serviceManager.EditAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        public ActionResult Employees()
        {
            return Json(serviceManager.Employees());
        }

        public ActionResult Equipment()
        {
            return Json(serviceManager.Equipments());
        }

        public ActionResult Locations()
        {
            return Json(serviceManager.Locations());
        }

        public ActionResult Parts()
        {
            return Json(serviceManager.Parts());
        }

        public ActionResult PermissionCodes()
        {
            return Json(serviceManager.PermissionCode());
        }

        public ActionResult Problems()
        {
            return Json(serviceManager.Problems());
        }

        public ActionResult RateSheets()
        {
            return Json(serviceManager.RateSheet());
        }

        public ActionResult Repairs()
        {
            return Json(serviceManager.Repairs());
        }

        [GET("api/v1/sm/workorders/{id}")]
        public ActionResult Workorders(string id)
        {
            return id == null ? Json(serviceManager.WorkOrders()) : Json(serviceManager.WorkOrders(id));
        }

        public ActionResult Workorders()
        {
            return Json(serviceManager.WorkOrders());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Workorders(SagePropertyDictionary properties)
        {
            try
            {
                return Json(serviceManager.WorkOrders(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }
    }
}