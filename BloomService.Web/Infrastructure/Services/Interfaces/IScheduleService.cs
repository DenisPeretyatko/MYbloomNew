using System;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface IScheduleService
    {
        ResponceModel CerateAssignment(AssignmentViewModel model);
        bool DeleteAssignment(AssignmentViewModel model);
        bool HasСrossoverAssignment(string employeeName, DateTime start, DateTime end, long wo);
        bool CheckEmployeeAvailableTime(SageEmployee employee, DateTime eventStart, DateTime eventEnd);
    }
}