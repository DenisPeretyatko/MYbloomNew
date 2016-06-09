namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using BloomService.Web.Services.Abstract;

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

        [GET("Workorder")]
        public ActionResult GetWorkorders()
        {
            var list = workOrderService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
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