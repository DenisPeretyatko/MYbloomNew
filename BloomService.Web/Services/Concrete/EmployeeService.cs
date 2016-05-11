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
    using BloomService.Web.Services.Concrete.EntityServices;

    public class EmployeeService : AddableEditableEntityService<SageEmployee>, IEmployeeService
    {
        private readonly IEmployeeApiManager employeeApiManager;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeApiManager employeeApiManager)
            : base(unitOfWork, employeeApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.employeeApiManager = employeeApiManager;
            Repository = unitOfWork.Employees;

            EndPoint = ConfigurationManager.AppSettings["EmployeeEndPoint"];
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