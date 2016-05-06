namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class RepairRepository : EntityRepository<SageRepair>, IRepairRepository
    {
        public RepairRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}