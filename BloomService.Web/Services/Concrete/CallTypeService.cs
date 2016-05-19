namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class CallTypeService : EntityService<SageCallType>, ICallTypeService
    {
        private readonly ICallTypeApiManager callTypeApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public CallTypeService(IUnitOfWork unitOfWork, ICallTypeApiManager callTypeApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, callTypeApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.callTypeApiManager = callTypeApiManager;
            Repository = unitOfWork.CallTypes;
            _settings = bloomConfiguration;

            EndPoint = _settings.CallTypeEndPoint;
        }
    }
}