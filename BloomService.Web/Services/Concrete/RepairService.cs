namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Services.Concrete.EntityServices;

    public class RepairService : EntityService<SageRepair>, IRepairService
    {
        private readonly IRepairApiManager repairApiManager;

        private readonly IUnitOfWork unitOfWork;

        public RepairService(IUnitOfWork unitOfWork, IRepairApiManager repairApiManager)
            : base(unitOfWork, repairApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.repairApiManager = repairApiManager;
            Repository = unitOfWork.Repairs;

            EndPoint = ConfigurationManager.AppSettings["RepairEndPoint"];
        }
    }
}