using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api.Controllers
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class CustomersController : ApiController
    {
        private readonly ICustomerService customerSageApiService;

        public CustomersController(ICustomerService customerSageApiService)
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
    }
}
