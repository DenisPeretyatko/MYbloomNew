namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class PartService : EntityService<SagePart>, IPartService
    {
        private readonly IPartApiManager partApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public PartService(IUnitOfWork unitOfWork, IPartApiManager partApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, partApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.partApiManager = partApiManager;
            Repository = unitOfWork.Parts;
            _settings = bloomConfiguration;

            EndPoint = _settings.PartEndPoint;
        }
    }
}