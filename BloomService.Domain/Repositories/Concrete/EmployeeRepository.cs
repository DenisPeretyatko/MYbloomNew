namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class EmployeeRepository : EntityRepository<SageEmployee>, IEmployeeRepository
    {
        public EmployeeRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}