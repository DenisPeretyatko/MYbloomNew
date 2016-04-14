using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Services;

namespace BloomService.Web.Controllers
{
    public class WorkorderController : BaseController
    {
        private readonly IWorkOrderSageApiService _workOrderSageApiService;

        public WorkorderController(IWorkOrderSageApiService workOrderSageApiService)
        {
            _workOrderSageApiService = workOrderSageApiService;
        }

        [GET("Workorder")]
        public ActionResult GetWorkorders()
        {
            var list = _workOrderSageApiService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/CreateWorkOrder")]
        public ActionResult CreateWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/SaveWorkOrder/{id}")]
        public ActionResult SaveWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}