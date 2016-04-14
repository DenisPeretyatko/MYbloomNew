using BloomService.Domain.Entities;
using BloomService.Web.Services.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api
{
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

        public IEnumerable<SageAssignment> Post(Properties properties)
        {
            return assignmentSageApiService.Add(properties);
        }

        public IEnumerable<SageAssignment> Put(Properties properties)
        {
            return assignmentSageApiService.Edit(properties);
        }

        public IEnumerable<SageAssignment> Delete(Properties properties)
        {
            return assignmentSageApiService.Edit(properties);
        }
    }
}
