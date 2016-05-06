namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class WorkOrderRepository : EntityRepository<SageWorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}