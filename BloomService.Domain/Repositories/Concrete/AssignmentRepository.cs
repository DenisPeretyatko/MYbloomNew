namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class AssignmentRepository : EntityRepository<SageAssignment>, IAssignmentRepository
    {
        public AssignmentRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}