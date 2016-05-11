namespace BloomService.Domain.Repositories.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface IWorkOrderRepository : IRepository<SageWorkOrder>
    {
        bool Delete(SageWorkOrder entity);
    }
}