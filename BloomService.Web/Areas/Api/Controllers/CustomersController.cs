using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api.Controllers
{
    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class CustomersController : ApiController
    {
        private readonly ICustomerSageApiService customerSageApiService;

        public CustomersController(ICustomerSageApiService customerSageApiService)
        {
            this.customerSageApiService = customerSageApiService;
        }

        public IEnumerable<SageCustomer> Get()
        {
            return customerSageApiService.Get();
        }

        public SageCustomer Get(string id)
        {
            return customerSageApiService.Get(id);
        }

        public IEnumerable<SageCustomer> Post(Properties properties)
        {
            return customerSageApiService.Add(properties);
        }

        public IEnumerable<SageCustomer> Put(Properties properties)
        {
            return customerSageApiService.Edit(properties);
        }

        public IEnumerable<SageCustomer> Delete(Properties properties)
        {
            return customerSageApiService.Edit(properties);
        }
    }
}
