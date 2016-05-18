namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
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
            Repository = unitOfWork.GetEntities<TEntity>();
        }

        public string EndPoint { get; set; }

        protected IRepository<TEntity> Repository { get; set; }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = Repository.Get();

            if (items.Any())
            {
                return items;
            }

            var entities = sageApiManager.Get(EndPoint);

            foreach (var entity in entities)
            {
                Repository.Add(entity);
            }

            return entities;
        }

        public virtual TEntity Get(string id)
        {
            var item = Repository.Get(id);

            if (item != null)
            {
                return item;
            }

            var entity = sageApiManager.Get(EndPoint, id);

            Repository.Add(entity);

            return entity;
        }

        protected string GetEntityId(SagePropertyDictionary sageProperties)
        {
            return "";//GetType().GetProperty(GetEntityName()).GetValue(this, null).ToString();
        }

        protected string GetEntityName()
        {
            return typeof(TEntity).Name;
        }
    }
}