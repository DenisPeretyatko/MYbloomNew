namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class DepartmentService : EntityService<SageDepartment>, IDepartmentService
    {
        private readonly IDepartmentApiManager departmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentApiManager departmentApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, departmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.departmentApiManager = departmentApiManager;
            Repository = unitOfWork.Departments;
            _settings = bloomConfiguration;

            EndPoint = _settings.DepartmentEndPoint;
        }
    }
}