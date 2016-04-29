using BloomService.Domain.Attributes;
using BloomService.Domain.Entities;
using BloomService.Domain.Repositories;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Domain.Repositories.Concrete;
using MongoDB.Driver;
using System.Linq;

namespace BloomService.Domain.UnitOfWork
{
    public class MongoDbUnitOfWork : IUnitOfWork
    {
        private IWorkOrderRepository workOrderRepository;

        static MongoUrl url = new MongoUrl("mongodb://localhost/MyDatabase");

        public IWorkOrderRepository WorkOrders
        {
            get
            {
                return workOrderRepository ?? (workOrderRepository = new WorkOrderRepository());
            }
        }

        public IRepository<TEntity> GetEntities<TEntity>() where TEntity : class, IEntity
        {
            var entityRepository = new MongoRepository<TEntity>(GetCollectionName<TEntity>());
            return entityRepository;
        }

        public string GetCollectionName<TEntity>() where TEntity : class, IEntity
        {
            var dnAttribute = typeof(TEntity).GetCustomAttributes(
                typeof(CollectionNameAttribute), true
            ).FirstOrDefault() as CollectionNameAttribute;
            if (dnAttribute != null)
            {
                return dnAttribute.Name;
            }
            return null;
        }
    }
}
