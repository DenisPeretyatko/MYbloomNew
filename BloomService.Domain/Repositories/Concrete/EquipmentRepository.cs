namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class EquipmentRepository : EntityRepository<SageEquipment>, IEquipmentRepository
    {
        public EquipmentRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}