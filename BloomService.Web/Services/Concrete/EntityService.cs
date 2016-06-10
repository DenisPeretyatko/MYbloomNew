namespace BloomService.Web.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
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

        public string EndPoint { get; set; }

        protected IRepository<TEntity> Repository { get; set; }

        public virtual IEnumerable<TEntity> Add(SagePropertyDictionary properties)
        {
            var result = sageApiManager.Add(EndPoint, properties);

            unitOfWork.Changes.Add(
                new SageChange
                {
                    Change = ChangeType.Create,
                    EntityId = GetEntityId(properties),
                    EntityType = GetEntityName(),
                    Status = StatusType.NotSynchronized,
                    ChangeTime = DateTime.UtcNow
                });

            return result;
        }

        public virtual IEnumerable<TEntity> Delete(string id)
        {
            var result = sageApiManager.Delete(EndPoint, id);

            unitOfWork.Changes.Add(
                new SageChange
                {
                    Change = ChangeType.Delete,
                    EntityId = id,
                    EntityType = GetEntityName(),
                    Status = StatusType.NotSynchronized,
                    ChangeTime = DateTime.UtcNow
                });

            return result;
        }

        public virtual IEnumerable<TEntity> Edit(SagePropertyDictionary properties)
        {
            var result = sageApiManager.Edit(EndPoint, properties);

            unitOfWork.Changes.Add(
                new SageChange
                    {
                        Change = ChangeType.Update, 
                        EntityId = GetEntityId(properties), 
                        EntityType = GetEntityName(), 
                        Status = StatusType.NotSynchronized, 
                        ChangeTime = DateTime.UtcNow
                    });
            //Repository.UpdateEntity(result.Single());

            return result;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = Repository.Get().ToArray();

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
            var entities = sageApiManager.Get(EndPoint);
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

            var entity = sageApiManager.Get(EndPoint, id);

            Repository.Add(entity);

            return entity;
        }

        protected string GetEntityId(SagePropertyDictionary sageProperties)
        {
            return sageProperties.SingleOrDefault(x => x.Key == GetEntityName()).Value;
        }

        protected string GetEntityName()
        {
            return typeof(TEntity).Name.Replace("Sage", "");
        }
    }
}