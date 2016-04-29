namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentService departmentSageApiService;

        public DepartmentsController(IDepartmentService departmentSageApiService)
        {
            this.departmentSageApiService = departmentSageApiService;
        }

        public IEnumerable<SageDepartment> Get()
        {
            return departmentSageApiService.Get();
        }

        public SageDepartment Get(string id)
        {
            return departmentSageApiService.Get(id);
        }
    }
}
