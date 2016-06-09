namespace BloomService.Web.Services.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Extensions;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class PartService : EntityService<SagePart>, IPartService
    {
        private readonly IPartApiManager partApiManager;

        private readonly IUnitOfWork unitOfWork;

        public PartService(
            IUnitOfWork unitOfWork, 
            IPartApiManager partApiManager, 
            BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, partApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.partApiManager = partApiManager;
            Repository = unitOfWork.Parts;

            EndPoint = bloomConfiguration.PartEndPoint;
        }
    }
}