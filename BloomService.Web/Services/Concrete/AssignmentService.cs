namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Services.Concrete.EntityServices;

    public class AssignmentService : AddableEditableEntityService<SageAssignment>, IAssignmentService
    {
        private readonly IAssignmentApiManager assignmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        public AssignmentService(IUnitOfWork unitOfWork, IAssignmentApiManager assignmentApiManager)
            : base(unitOfWork, assignmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.assignmentApiManager = assignmentApiManager;

            EndPoint = ConfigurationManager.AppSettings["AssignmentEndPoint"];
        }
    }
}