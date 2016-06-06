namespace BloomService.Domain.Repositories.Concrete
{
    using Entities.Concrete;
    using Abstract;

    public class TechnicianLocationRepository : EntityRepository<SageTechnicianLocation>, ITechnicianLocationRepository
    {
        public TechnicianLocationRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}