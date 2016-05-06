namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class EmployeesController : ApiController
    {
        private readonly IEmployeeService employeeSageApiService;

        public EmployeesController(IEmployeeService employeeSageApiService)
        {
            this.employeeSageApiService = employeeSageApiService;
        }

        public IEnumerable<SageEmployee> Get()
        {
            return employeeSageApiService.Get();
        }

        public SageEmployee Get(string id)
        {
            return employeeSageApiService.Get(id);
        }
    }
}
