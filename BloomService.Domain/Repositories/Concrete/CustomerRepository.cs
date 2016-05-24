using System.Linq;

namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class CustomerRepository : EntityRepository<SageCustomer>, ICustomerRepository
    {
        public CustomerRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}