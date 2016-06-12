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

    public class EmployeeService : EntityService<SageEmployee>, IEmployeeService
    {
        private readonly IEmployeeApiManager employeeApiManager;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeApiManager employeeApiManager)
            : base(unitOfWork, employeeApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.employeeApiManager = employeeApiManager;
            Repository = unitOfWork.Employees;
        }

        public IEnumerable<SageEmployee> EditToMongo(SageEmployee employee)
        {
            var hasError = Repository.Add(employee);

            if (!hasError)
            {
                unitOfWork.Changes.Add(ChangeType.Update, employee.Employee, GetEntityName());
            }

            var result = Repository.Get();
            return result;
        }
    }
}