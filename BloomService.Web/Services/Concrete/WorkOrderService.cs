using System.Linq;

namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Extensions;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class WorkOrderService : EntityService<SageWorkOrder>, IWorkOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IWorkOrderApiManager workOrderApiManager;

        public WorkOrderService(
            IUnitOfWork unitOfWork, 
            IWorkOrderApiManager workOrderApiManager)
            : base(unitOfWork, workOrderApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.workOrderApiManager = workOrderApiManager;
            Repository = unitOfWork.WorkOrders;
        }

        public override SageResponse<SageWorkOrder> Add(SageWorkOrder workOrder)
        {
            var result = workOrderApiManager.Add(workOrder);
            if (result != null && result.IsSucceed)
            {
                unitOfWork.WorkOrders.Add(workOrder);
                unitOfWork.Changes.Add(ChangeType.Create, workOrder.WorkOrder, GetEntityName());
            }

            return result;
        }

        public override SageResponse<SageWorkOrder> Edit(SageWorkOrder workOrder)
        {
            var result = workOrderApiManager.Add(workOrder);

            if (result != null && result.IsSucceed)
            {
                unitOfWork.WorkOrders.Add(workOrder);
                unitOfWork.Changes.Add(ChangeType.Create, workOrder.WorkOrder, GetEntityName());
            }

            return result;
        }

        public IEnumerable<SageWorkOrder> UnAssign(string id)
        {
            return workOrderApiManager.Delete(id).Entities;
        }

        public SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties)
        {
            return workOrderApiManager.AddEquipment(properties);
        }

        public SageImageWorkOrder GetPictures(string id)
        {
            var pictureLocation = unitOfWork.GetEntities<SageImageWorkOrder>().SearchFor(x => x.WorkOrderBsonId == id);
            return pictureLocation.Any() ? pictureLocation.Single() : null;
        }
    }
}