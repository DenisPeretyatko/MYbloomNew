namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    public interface IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        SageResponse<TEntity> Add(TEntity entity);

        SageResponse<TEntity> Delete(string id);

        SageResponse<TEntity> Edit(TEntity entity);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> GetPage(int numberPage);

        int CountPage();

        TEntity Get(string id);
    }
}