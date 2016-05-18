namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Services.Concrete.EntityServices;

    public class PartService : EntityService<SagePart>, IPartService
    {
        private readonly IPartApiManager partApiManager;

        private readonly IUnitOfWork unitOfWork;

        public PartService(IUnitOfWork unitOfWork, IPartApiManager partApiManager)
            : base(unitOfWork, partApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.partApiManager = partApiManager;
            Repository = unitOfWork.Parts;

            EndPoint = ConfigurationManager.AppSettings["PartEndPoint"];
        }
    }
}