using System.Linq;

namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class PartRepository : EntityRepository<SagePart>, IPartRepository
    {
        public PartRepository(string collectionName)
            : base(collectionName)
        {
        }
        public override IQueryable<SagePart> Get()
        {
            return base.Get().Take(20);
        }
    }
}