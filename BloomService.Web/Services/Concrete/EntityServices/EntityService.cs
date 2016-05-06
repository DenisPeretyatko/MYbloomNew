namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Services.Abstract.EntityServices;

    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IEntityApiManager<TEntity> sageApiManager;

        private readonly IUnitOfWork unitOfWork;

        public EntityService(IUnitOfWork unitOfWork, IEntityApiManager<TEntity> sageApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.sageApiManager = sageApiManager;
        }

        public string EndPoint { get; set; }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = unitOfWork.GetEntities<TEntity>().Get().Take(20).ToArray();

            if (items.Any())
            {
                return items;
            }

            var entities = sageApiManager.Get(EndPoint);

            foreach (var entity in entities)
            {
                unitOfWork.GetEntities<TEntity>().Insert(entity);
            }

            return entities;
        }

        public virtual TEntity Get(string id)
        {
            var item = unitOfWork.GetEntities<TEntity>().Get(id);

            if (item != null)
            {
                return item;
            }

            var entity = sageApiManager.Get(EndPoint, id);

            unitOfWork.GetEntities<TEntity>().Insert(entity);

            return entity;
        }
    }
}