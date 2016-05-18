using System.Linq;

namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;
    using System.Linq;

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
            Repository = unitOfWork.Assignments;

            EndPoint = ConfigurationManager.AppSettings["AssignmentEndPoint"];
        }

        public virtual SageAssignment GetByWorkOrderId(string id)
        {
            //var item = unitOfWork.Assignments.SearchFor(a => a.WorkOrder == id).SingleOrDefault();

            //if (item != null)
            //{
            //    return item;
            //}

            var entity = assignmentApiManager.Get(EndPoint).SingleOrDefault(a => a.WorkOrder == id);

            if (entity != null)
            {
                Repository.Add(entity);
            }

            return entity;
        }
    }
}