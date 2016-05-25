namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class AssignmentsController : ApiController
    {
        private readonly IAssignmentService assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
        }

        public IEnumerable<SageAssignment> Get()
        {
            return assignmentService.Get();
        }

        public SageAssignment Get(string id)
        {
            return assignmentService.Get(id);
        }

        public IEnumerable<SageAssignment> Post(Dictionary<string, string> properties)
        {
            return assignmentService.Add(properties);
        }

        public IEnumerable<SageAssignment> Put(Dictionary<string, string> properties)
        {
            return assignmentService.Edit(properties);
        }
    }
}