namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class ProblemRepository : EntityRepository<SageProblem>, IProblemRepository
    {
        public ProblemRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}