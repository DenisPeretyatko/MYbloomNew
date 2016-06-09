namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class, Domain.Entities.Abstract.IEntity
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

        public virtual SageResponse<TEntity> Add(TEntity entity)
        {
            var result = sageApiManager.Add(entity);
            return result;
        }

        public virtual SageResponse<TEntity> Delete(string id)
        {
            var result = sageApiManager.Delete(id);
            if (result.IsSucceed)
            {
                unitOfWork.Changes.Add(ChangeType.Delete, id, GetEntityName());
            }

            return result;
        }

        public virtual SageResponse<TEntity> Edit(TEntity entity)
        {
            var result = sageApiManager.Edit(entity);
            return result;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = Repository.Get().Take(20).ToArray();

            if (items.Any())
            {
                return items;
            }

            var entities = sageApiManager.Get();

            foreach (var entity in entities.Entities)
            {
                Repository.Add(entity);
            }

            return entities.Entities;
        }

        public virtual TEntity Get(string id)
        {
            var item = Repository.Get(id);

            if (item != null)
            {
                return item;
            }

            var entity = sageApiManager.Get(id);

            Repository.Add(entity.Entity);

            return entity.Entity;
        }

        protected string GetEntityId(Dictionary<string, string> sageProperties)
        {
            return sageProperties.SingleOrDefault(x => x.Key == GetEntityName()).Value;
        }

        protected string GetEntityName()
        {
            return typeof(TEntity).Name.Replace("Sage", string.Empty);
        }
    }
}