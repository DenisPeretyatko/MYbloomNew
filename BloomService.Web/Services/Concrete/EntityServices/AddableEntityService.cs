namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Services.Abstract.EntityServices;

    public class AddableEntityService<TEntity> : EntityService<TEntity>, IAddableEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IAddableEntityApiManager<TEntity> sageApiManager;

        private readonly IUnitOfWork unitOfWork;

        public AddableEntityService(IUnitOfWork unitOfWork, IAddableEntityApiManager<TEntity> sageApiManager)
            : base(unitOfWork, sageApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.sageApiManager = sageApiManager;
        }

        public virtual IEnumerable<TEntity> Add(PropertyDictionary properties)
        {
            return sageApiManager.Add(EndPoint, properties);
        }
    }
}