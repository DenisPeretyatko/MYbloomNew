namespace BloomService.Web.Managers.Abstract.EntityManagers
{
    using BloomService.Domain.Entities.Abstract;

    public interface IAddableEditableApiManager<TEntity> : IAddableEntityApiManager<TEntity>, 
                                                                 IEditableEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
    }
}