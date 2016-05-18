﻿namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class CallTypeService : EntityService<SageCallType>, ICallTypeService
    {
        private readonly ICallTypeApiManager callTypeApiManager;

        private readonly IUnitOfWork unitOfWork;

        public CallTypeService(IUnitOfWork unitOfWork, ICallTypeApiManager callTypeApiManager)
            : base(unitOfWork, callTypeApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.callTypeApiManager = callTypeApiManager;
            Repository = unitOfWork.CallTypes;

            EndPoint = ConfigurationManager.AppSettings["CallTypeEndPoint"];
        }
    }
}