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
    using Domain.Extensions;

    public class AssignmentService : EntityService<SageAssignment>, IAssignmentService
    {
        private readonly IAssignmentApiManager assignmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public AssignmentService(IUnitOfWork unitOfWork, IAssignmentApiManager assignmentApiManager, BloomServiceConfiguration bloomCobfiguration)
            : base(unitOfWork, assignmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.assignmentApiManager = assignmentApiManager;
            Repository = unitOfWork.Assignments;
            _settings = bloomCobfiguration;

            EndPoint = _settings.AssignmentEndPoint;
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