namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class ChangeRepository : EntityRepository<SageChange>, IChangeRepository
    {
        public ChangeRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}