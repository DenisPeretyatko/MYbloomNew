using System.Linq;
using BloomService.Web.Services.Concrete;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using System.Linq;
    using BloomService.Web.Services.Abstract;


    [Authorize]
    public class WorkorderController : BaseController
    {
        private readonly IWorkOrderService workOrderService;

        public WorkorderController(IWorkOrderService workOrderService)
        {
            this.workOrderService = workOrderService;
        }

        [POST("Workorder/Create")]
        public ActionResult CreateWorkOrder(WorkOrderModel model)
        {
            var workOrder = AutoMapper.Mapper.Map<SageWorkOrder>(model);

            var created = workOrderService.Add(workOrder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [GET("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workOrder = workOrderService.Get(id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [GET("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(string id)
        {
            var pictures = workOrderService.GetPictures(id);
            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        [GET("Workorder")]
        public ActionResult GetWorkorders()
        {
            var list = workOrderService.Get().Skip(11220).Take(20);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [GET("WorkorderPage")]
        public ActionResult GetWorkorderPage(int index, string searchString)
        {
            var list = workOrderService.GetPage(index, searchString);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public class Count
        {
            public int CountPage { get; set; }
        }

        [GET("WorkorderPageCount/{searchString}")]
        public ActionResult GetWorkorderPageCount(string searchString)
        {
            var result = new Count() { CountPage = workOrderService.CountPage(searchString) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            var workOrder = AutoMapper.Mapper.Map<SageWorkOrder>(model);
            var saved = workOrderService.Edit(workOrder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}