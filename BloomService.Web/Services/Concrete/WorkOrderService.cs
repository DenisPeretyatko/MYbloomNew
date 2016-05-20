namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class WorkOrderService : EntityService<SageWorkOrder>, IWorkOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IWorkOrderApiManager workOrderApiManager;

        private readonly BloomServiceConfiguration _settings;

        public WorkOrderService(IUnitOfWork unitOfWork, IWorkOrderApiManager workOrderApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, workOrderApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.workOrderApiManager = workOrderApiManager;
            Repository = unitOfWork.WorkOrders;
            _settings = bloomConfiguration;
            EndPoint = _settings.WorkOrderEndPoint;
        }

        public IEnumerable<SageWorkOrder> UnAssign(string id) 
        {
            return workOrderApiManager.Delete(EndPoint, id);
        }
    }
}