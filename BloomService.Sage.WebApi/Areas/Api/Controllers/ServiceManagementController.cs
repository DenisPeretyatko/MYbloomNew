using AttributeRouting.Web.Mvc;
using Sage.WebApi.Infratructure.Service;
using System.Web.Mvc;

namespace Sage.WebApi.Areas.Api.Controllers
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Exceptions;

    [Authorize]
    public class ServiceManagementController : BaseApiController
    {
        private readonly IServiceManagement _serviceManager;

        public ServiceManagementController(IServiceManagement serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        public ActionResult Locations()
        {
            return Json(_serviceManager.Locations());
        }

        public ActionResult Parts()
        {
            return Json(_serviceManager.Parts());
        }

        public ActionResult Problems()
        {
            return Json(_serviceManager.Problems());
        }

        public ActionResult Repairs()
        {
            return Json(_serviceManager.Repairs());
        }

        public ActionResult Employees()
        {
            return Json(_serviceManager.Employees());
        }
        
        [GET("api/v1/sm/workorders/{id}")]
        public ActionResult Workorders(string id)
        {
            return id == null ? Json(_serviceManager.WorkOrders()) : Json(_serviceManager.WorkOrders(id));
        }

        public ActionResult Workorders()
        {
            return Json(_serviceManager.WorkOrders());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Workorders(PropertyDictionary properties)
        {
            try
            {
                return Json(_serviceManager.WorkOrders(properties));
            }
            catch(ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        public ActionResult Calltypes()
        {
            return Json(_serviceManager.Calltypes());
        }

        public ActionResult Departments()
        {
            return Json(_serviceManager.Departments());
        }

        public ActionResult Equipment()
        {
            return Json(_serviceManager.Equipments());
        }

        [GET("api/v1/sm/assignments/{id}")]
        public ActionResult Assignments(string id)
        {
            return id == null ? Json(_serviceManager.Assignments()) : Json(_serviceManager.Assignments(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAssignments(PropertyDictionary properties)
        {
            try
            {
                return Json(_serviceManager.AddAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAssignments(PropertyDictionary properties)
        {
            try
            {
                return Json(_serviceManager.EditAssignments(properties));
            }
            catch (ResponseException ex)
            {
                return Json(ex.Error);
            }
        }
        
        public ActionResult RateSheets()
        {
            return Json(_serviceManager.RateSheet());
        }
        
        public ActionResult PermissionCodes()
        {
            return Json(_serviceManager.PermissionCode());
        }

        public ActionResult Agreements()
        {
            return Json(_serviceManager.Agreements());
        }
    }
}