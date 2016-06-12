﻿namespace BloomService.Web.Services.Concrete.EntityServices
{
    using System;
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract.EntityServices;

    public class AddableEditableEntityService<TEntity> : EntityService<TEntity>, IAddableEditableEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IEntityApiManager<TEntity> sageApiManager;

        private readonly IUnitOfWork unitOfWork;

        public AddableEditableEntityService(IUnitOfWork unitOfWork, IEntityApiManager<TEntity> sageApiManager)
            : base(unitOfWork, sageApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.sageApiManager = sageApiManager;
        }

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

            return result;
        }
    }
}