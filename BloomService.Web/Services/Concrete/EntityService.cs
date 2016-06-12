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
            var items = Repository.Get().ToArray();

            if (items.Any())
            {
                return items;
            }

            var response = sageApiManager.Get();
                        
            if (response.IsSucceed)
            {
                var entities = response.Entities;
                foreach (var entity in entities)
                {
                    Repository.Add(entity);
                }
                return entities;
            }
            
            return new List<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetPage(int numberPage)
        {
            if(numberPage == 0)
                return new TEntity[0];
            var items = Repository.Get().ToList();

            if (items.Any())
            {
                var result = new TEntity[12];
                var index = (numberPage - 1) * 12;
                if(index > items.Count())
                    return new TEntity[0];
                var max = (index + 12) > items.Count() ? (index + 12) - items.Count : 12;
                items.CopyTo(index, result, 0, max);
                return result;
            }
            var entities = sageApiManager.Get().Entities;
            foreach (var entity in entities)
            {
                Repository.Add(entity);
            }

            return entities;
        }

        public virtual int CountPage()
        {
            var items = Repository.Get().ToList();
            return items.Count / 12 - 1 + (items.Count % 12 == 0 ? 0 : 1);
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