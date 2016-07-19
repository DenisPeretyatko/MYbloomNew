using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface IScheduleService
    {
        bool CerateAssignment(AssignmentViewModel model);
        bool DeleteAssignment(AssignmentViewModel model);
    }
}