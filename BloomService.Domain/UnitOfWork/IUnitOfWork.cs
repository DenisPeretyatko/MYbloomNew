using BloomService.Domain.Entities;
using BloomService.Domain.Repositories.Abstract;

namespace BloomService.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IWorkOrderRepository WorkOrders { get; }

        IRepository<TEntity> GetEntities<TEntity>() where TEntity : class, IEntity;
    }
}
