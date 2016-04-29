namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class AssignmentsController : ApiController
    {
        private readonly IAssignmentSageApiService assignmentSageApiService;

        public AssignmentsController(IAssignmentSageApiService assignmentSageApiService)
        {
            this.assignmentSageApiService = assignmentSageApiService;
        }

        public IEnumerable<SageAssignment> Get()
        {
            return assignmentSageApiService.Get();
        }

        public SageAssignment Get(string id)
        {
            return assignmentSageApiService.Get(id);
        }

        public IEnumerable<SageAssignment> Post(PropertyDictionary properties)
        {
            return assignmentSageApiService.Add(properties);
        }

        public IEnumerable<SageAssignment> Put(PropertyDictionary properties)
        {
            return assignmentSageApiService.Edit(properties);
        }

        public IEnumerable<SageAssignment> Delete(PropertyDictionary properties)
        {
            return assignmentSageApiService.Edit(properties);
        }
    }
}
