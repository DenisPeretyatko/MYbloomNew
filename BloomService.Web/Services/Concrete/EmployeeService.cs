namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
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

            EndPoint = ConfigurationManager.AppSettings["EmployeeEndPoint"];
        }
    }
}