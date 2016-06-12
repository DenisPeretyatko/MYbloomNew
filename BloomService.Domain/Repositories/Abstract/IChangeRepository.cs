namespace BloomService.Domain.Repositories.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using Entities.Concrete.Auxiliary;
    public interface IChangeRepository : IRepository<SageChange>
    {
        bool Add(ChangeType type, string id, string name);
    }
}