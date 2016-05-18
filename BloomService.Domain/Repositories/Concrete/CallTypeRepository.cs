using System.Linq;

namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class CallTypeRepository : EntityRepository<SageCallType>, ICallTypeRepository
    {
        public CallTypeRepository(string collectionName)
            : base(collectionName)
        {
        }

        public override IQueryable<SageCallType> Get()
        {
            return base.Get().Take(20);
        }
    }
}