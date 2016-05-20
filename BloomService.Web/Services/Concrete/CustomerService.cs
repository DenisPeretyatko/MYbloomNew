﻿namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class CustomerService : EntityService<SageCustomer>, ICustomerService
    {
        private readonly ICustomerApiManager customerApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerApiManager customerApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, customerApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.customerApiManager = customerApiManager;
            Repository = unitOfWork.Customers;
            _settings = bloomConfiguration;

            EndPoint = _settings.CustomerEndPoint;
        }
    }
}