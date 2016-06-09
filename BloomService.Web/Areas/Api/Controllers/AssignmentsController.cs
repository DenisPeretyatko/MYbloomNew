namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IHttpActionResult Get()
        {
            var result = assignmentService.Get();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }

        public IHttpActionResult Get(string id)
        {
            var result = assignmentService.Get(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public IHttpActionResult Post(SageAssignment assignment)
        {
            var result = assignmentService.Add(assignment);
            if (result.IsSucceed)
            {
                return Ok(result.Entity);
            }

            return NotFound();
        }

        public IHttpActionResult Put(SageAssignment assignment)
        {
            var result = assignmentService.Edit(assignment);
            if (result.IsSucceed)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}