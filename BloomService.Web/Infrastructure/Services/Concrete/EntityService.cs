namespace BloomService.Web.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IEntityApiManager<TEntity> sageApiManager;
        private readonly IRepository _repository;

        private const int _itemsOnPage = 12;

        public EntityService(IEntityApiManager<TEntity> sageApiManager, IRepository repository)
        {
            this.sageApiManager = sageApiManager;
            _repository = repository;
        }

        public string EndPoint { get; set; }

        public virtual IEnumerable<TEntity> Add(SagePropertyDictionary properties)
        {
            var result = sageApiManager.Add(EndPoint, properties);

            var change = new SageChange
            {
                Change = ChangeType.Create,
                EntityId = GetEntityId(properties),
                EntityType = GetEntityName(),
                Status = StatusType.NotSynchronized,
                ChangeTime = DateTime.UtcNow
            };
            _repository.Add(change);

            return result;
        }

        public virtual IEnumerable<TEntity> Delete(string id)
        {
            var result = sageApiManager.Delete(EndPoint, id);

            var change = new SageChange
            {
                Change = ChangeType.Delete,
                EntityId = id,
                EntityType = GetEntityName(),
                Status = StatusType.NotSynchronized,
                ChangeTime = DateTime.UtcNow
            };
            _repository.Add(change);

            return result;
        }

        public virtual IEnumerable<TEntity> Edit(SagePropertyDictionary properties)
        {
            var result = sageApiManager.Edit(EndPoint, properties);

            var change = new SageChange
            {
                Change = ChangeType.Update,
                EntityId = GetEntityId(properties),
                EntityType = GetEntityName(),
                Status = StatusType.NotSynchronized,
                ChangeTime = DateTime.UtcNow
            };
            _repository.Add(change);

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
            var entitiesCount = Repository.Get().Count();
            if (entitiesCount == 0)
            {
                var entities = sageApiManager.Get(EndPoint);
                foreach (var entity in entities)
                {
                    Repository.Add(entity);
                }
            }
            return Repository.Get().Skip((numberPage - 1) * _itemsOnPage).Take(_itemsOnPage);
        }

        public virtual int CountPage()
        {
            var entitiesCount = Repository.Get().Count();
            return entitiesCount % _itemsOnPage == 0 ? entitiesCount / _itemsOnPage : entitiesCount / _itemsOnPage + 1;
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