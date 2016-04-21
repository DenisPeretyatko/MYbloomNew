using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Web.Models;
using BloomService.Web.Services;

namespace BloomService.Web.Controllers
{
    using BloomService.Web.Services.Abstract;

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

        [GET("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workOrder = _workOrderSageApiService.Get(id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/Create")]
        public ActionResult CreateWorkOrder(WorkOrderModel model)
        {
            var workorder = new PropertyDictionary
            {
                {"ARCustomer", model.Customer},
                {"Location", model.Location},
                {"CallType", model.Calltype},
                {"CallDate", model.Calldate.ToShortDateString()},
                {"CallTime", model.Calldate.ToShortTimeString()},
                {"Problem", model.Problem},
                {"RateSheet", model.Ratesheet},
                {"Employee", model.Emploee},
                {"Equipment", model.Equipment},
                {"EstimatedRepairHours", model.Estimatehours},
                {"NottoExceed", model.Nottoexceed},
                {"Comments", model.Locationcomments},
                {"CustomerPO", model.Customerpo},
                {"PermissionCode", model.Permissiocode},
                {"PayMethod", model.Paymentmethods}
            };

            var created = _workOrderSageApiService.Add(workorder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/Save/{id}")]
        public ActionResult SaveWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}