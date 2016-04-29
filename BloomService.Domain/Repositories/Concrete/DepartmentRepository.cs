namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class DepartmentRepository : EntityRepository<SageDepartment>, IDepartmentRepository
    {
        public DepartmentRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}