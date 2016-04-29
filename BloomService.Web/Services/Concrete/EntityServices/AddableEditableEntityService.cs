namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Services.Abstract.EntityServices;

    public class AddableEditableEntityService<TEntity> : EntityService<TEntity>, IAddableEditableEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IAddableEditableApiManager<TEntity> sageApiManager;

        private readonly IUnitOfWork unitOfWork;

        public AddableEditableEntityService(IUnitOfWork unitOfWork, IAddableEditableApiManager<TEntity> sageApiManager)
            : base(unitOfWork, sageApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.sageApiManager = sageApiManager;
        }

        public virtual IEnumerable<TEntity> Add(PropertyDictionary properties)
        {
            return sageApiManager.Add(EndPoint, properties);
        }

        public virtual IEnumerable<TEntity> Edit(PropertyDictionary properties)
        {
            return sageApiManager.Edit(EndPoint, properties);
        }
    }
}