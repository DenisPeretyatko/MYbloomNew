namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class ProblemService : EntityService<SageProblem>, IProblemService
    {
        private readonly IProblemApiManager problemApiManager;

        private readonly IUnitOfWork unitOfWork;

        public ProblemService(IUnitOfWork unitOfWork, IProblemApiManager problemApiManager)
            : base(unitOfWork, problemApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.problemApiManager = problemApiManager;
            Repository = unitOfWork.Problems;
        }
    }
}