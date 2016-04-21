namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class EmployeesController : ApiController
    {
        private readonly IEmployeeSageApiService employeeSageApiService;

        public EmployeesController(IEmployeeSageApiService employeeSageApiService)
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

        public IEnumerable<SageEmployee> Post(PropertyDictionary properties)
        {
            return employeeSageApiService.Add(properties);
        }

        public IEnumerable<SageEmployee> Put(PropertyDictionary properties)
        {
            return employeeSageApiService.Edit(properties);
        }

        public IEnumerable<SageEmployee> Delete(PropertyDictionary properties)
        {
            return employeeSageApiService.Edit(properties);
        }
    }
}
