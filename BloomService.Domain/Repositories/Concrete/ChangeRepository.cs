namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
    using Entities.Concrete.Auxiliary;
    using System;
    public class ChangeRepository : EntityRepository<SageChange>, IChangeRepository
    {
        public ChangeRepository(string collectionName)
            : base(collectionName)
        {
        }

        public bool Add(ChangeType type, string id, string name)
        {
            var change = new SageChange()
            {
                Change = type,
                EntityId = id,
                EntityType = name,
                Status = StatusType.NotSynchronized,
                ChangeTime = DateTime.UtcNow.ToString()
            };

            return base.Add(change);
        }
    }
}