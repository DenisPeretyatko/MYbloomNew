using BloomService.Domain.Entities;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Domain.Repositories.Concrete;

namespace BloomService.Domain.Repositories
{
    public class WorkOrderRepository : MongoRepository<SageWorkOrder>, IWorkOrderRepository
    {
        private static string collectionName = "WorkOrderCollection";

        public WorkOrderRepository() : base(collectionName)
        {
            
        }
    }
}
