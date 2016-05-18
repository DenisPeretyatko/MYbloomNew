namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class WorkOrderService : EntityService<SageWorkOrder>, IWorkOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IWorkOrderApiManager workOrderApiManager;

        public WorkOrderService(IUnitOfWork unitOfWork, IWorkOrderApiManager workOrderApiManager)
            : base(unitOfWork, workOrderApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.workOrderApiManager = workOrderApiManager;

            EndPoint = ConfigurationManager.AppSettings["WorkOrderEndPoint"];
        }
    }
}