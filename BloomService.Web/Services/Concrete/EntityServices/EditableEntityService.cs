namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Services.Abstract.EntityServices;

    public class EditableEntityService<TEntity> : EntityService<TEntity>, IEditableEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IEditableEntityApiManager<TEntity> sageApiManager;

        private readonly IUnitOfWork unitOfWork;

        public EditableEntityService(IUnitOfWork unitOfWork, IEditableEntityApiManager<TEntity> sageApiManager)
            : base(unitOfWork, sageApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.sageApiManager = sageApiManager;
        }

        public virtual IEnumerable<TEntity> Edit(SagePropertyDictionary properties)
        {
            return sageApiManager.Edit(EndPoint, properties);
        }
    }
}