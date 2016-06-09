namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    public interface IEntityApiManager<TEntity>
        where TEntity : IEntity
    {
        SageResponse<TEntity> Add(TEntity entity);

        SageResponse<TEntity> Delete(string id);

        SageResponse<TEntity> Edit(TEntity entity);

        SageResponse<TEntity> Get();

        SageResponse<TEntity> Get(string id);
    }
}