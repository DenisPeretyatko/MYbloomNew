namespace BloomService.Web.Services.Concrete
{
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Extensions;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class AssignmentService : EntityService<SageAssignment>, IAssignmentService
    {
        private readonly IAssignmentApiManager assignmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        public AssignmentService(
            IUnitOfWork unitOfWork, 
            IAssignmentApiManager assignmentApiManager, 
            BloomServiceConfiguration bloomCobfiguration)
            : base(unitOfWork, assignmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.assignmentApiManager = assignmentApiManager;
            Repository = unitOfWork.Assignments;

            EndPoint = bloomCobfiguration.AssignmentEndPoint;
        }

        public virtual SageAssignment GetByWorkOrderId(string id)
        {
            var item = unitOfWork.Assignments.SearchFor(a => a.WorkOrder == id).SingleOrDefault();

            if (item != null)
            {
                return item;
            }

            var entity = assignmentApiManager.Get().Entities.SingleOrDefault(a => a.WorkOrder == id);

            if (entity != null)
            {
                Repository.Add(entity);
            }

            return entity;
        }
    }
}