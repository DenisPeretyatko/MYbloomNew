namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;

    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class EmployeeService : EntityService<SageEmployee>, IEmployeeService
    {
        private readonly IEmployeeApiManager employeeApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeApiManager employeeApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, employeeApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.employeeApiManager = employeeApiManager;
            Repository = unitOfWork.Employees;
            _settings = bloomConfiguration;

            EndPoint = _settings.EmployeeEndPoint;
        }

        public IEnumerable<SageEmployee> EditToMongo(SageEmployee employee)
        {
            var hasError = Repository.Add(employee);

            if (!hasError)
            {
                unitOfWork.Changes.Add(
                    new SageChange
                        {
                            Change = ChangeType.Update, 
                            EntityId = employee.Employee,
                            EntityType = GetEntityName(), 
                            Status = StatusType.NotSynchronized
                        });
            }

            var result = Repository.Get();
            return result;
        }
    }
}