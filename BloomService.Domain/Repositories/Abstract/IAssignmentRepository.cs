namespace BloomService.Domain.Repositories.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface IAssignmentRepository : IRepository<SageAssignment>
    {
        bool Delete(SageAssignment entity);

        bool Update(SageAssignment entity);
    }
}