using System.Linq;

namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class AssignmentService : EntityService<SageAssignment>, IAssignmentService
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

        public virtual SageAssignment GetByWorkOrderId(string id)
        {
            var item = unitOfWork.Assignments.SearchFor(a => a.WorkOrder == id).SingleOrDefault();

            if (item != null)
            {
                return item;
            }

            var entity = assignmentApiManager.Get(EndPoint).SingleOrDefault(a => a.WorkOrder == id);

            if (entity != null)
            {
                Repository.Add(entity);
            }

            return entity;
        }
    }
}