namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class DepartmentService : EntityService<SageDepartment>, IDepartmentService
    {
        private readonly IDepartmentApiManager departmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentApiManager departmentApiManager)
            : base(unitOfWork, departmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.departmentApiManager = departmentApiManager;
            Repository = unitOfWork.Departments;

            EndPoint = ConfigurationManager.AppSettings["DepartmentEndPoint"];
        }
    }
}