using System.Linq;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using System.Linq;
    using Domain.Repositories.Abstract;
    using System;

    [Authorize]
    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;

        public WorkorderController(IRepository repository)
        {
            _repository = repository;
        }

        [POST("Workorder/Create")]
        public ActionResult CreateWorkOrder(WorkOrderModel model)
        {
            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.Date,
                CallTime = model.Calldate,
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee,
                Equipment = Convert.ToUInt16(model.Equipment),
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods
            };

            _repository.Add(workorder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [GET("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workOrder = _repository.Get<SageWorkOrder>(id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [GET("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(string id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrderBsonId == id).Single();
            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        [GET("Workorder")]
        public ActionResult GetWorkorders()
        {
            var list = _repository.GetAll<SageWorkOrder>(); //workOrderService.Get().Skip(11220).Take(20);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [GET("WorkorderPage/{index}")]
        public ActionResult GetWorkorderPage(int index)
        {
            var list = workOrderService.GetPage(index);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public class Count
        {
            public int CountPage { get; set; }
        }

        [GET("WorkorderPageCount")]
        public ActionResult GetWorkorderPageCount()
        {
            var result = new Count() { CountPage = workOrderService.CountPage() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            //var workorder = new SagePropertyDictionary
            //{
            //                        { "ARCustomer", model.Customer }, 
            //                        { "Location", model.Location }, 
            //                        { "CallType", model.Calltype }, 
            //                        { "CallDate", model.Calldate.ToShortDateString() }, 
            //                        { "CallTime", model.Calldate.ToShortTimeString() }, 
            //                        { "Problem", model.Problem }, 
            //                        { "RateSheet", model.Ratesheet }, 
            //                        { "Employee", model.Emploee }, 
            //                        { "Equipment", model.Equipment }, 
            //                        { "EstimatedRepairHours", model.Estimatehours }, 
            //                        { "NottoExceed", model.Nottoexceed }, 
            //                        { "Comments", model.Locationcomments }, 
            //                        { "CustomerPO", model.Customerpo }, 
            //                        { "PermissionCode", model.Permissiocode }, 
            //                        { "PayMethod", model.Paymentmethods },
            //                        { "WorkOrder", model.WorkOrder }
            //};

            var workorder = _repository.Get<SageWorkOrder>(model.Id);
            _repository.Update(workorder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}