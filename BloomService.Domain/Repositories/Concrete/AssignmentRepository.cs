using System.Linq;

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
        public override IQueryable<SageAssignment> Get()
        {
            return base.Get().Skip(10445).Take(20);
        }
    }
}