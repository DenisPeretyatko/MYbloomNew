namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Repositories.Abstract;

    public class WorkOrderRepository : MongoRepository<SageWorkOrder>, IWorkOrderRepository
    {
        private const string CollectionName = "WorkOrderCollection";

        public WorkOrderRepository()
            : base(CollectionName)
        {
        }
    }
}