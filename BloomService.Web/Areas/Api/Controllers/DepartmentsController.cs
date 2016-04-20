namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentSageApiService departmentSageApiService;

        public DepartmentsController(IDepartmentSageApiService departmentSageApiService)
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

        public IEnumerable<SageDepartment> Post(PropertyDictionary properties)
        {
            return departmentSageApiService.Add(properties);
        }

        public IEnumerable<SageDepartment> Put(PropertyDictionary properties)
        {
            return departmentSageApiService.Edit(properties);
        }

        public IEnumerable<SageDepartment> Delete(PropertyDictionary properties)
        {
            return departmentSageApiService.Edit(properties);
        }
    }
}
