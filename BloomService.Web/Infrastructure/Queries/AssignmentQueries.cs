using BloomService.Domain.Entities.Concrete;
using System.Linq;

namespace BloomService.Web.Infrastructure.Queries
{
    public static class AssignmentQueries
    {
        public static IQueryable<SageAssignment> Unassigned(this IQueryable<SageAssignment> query)
        {
            return query.Where(x => x.Employee == "");
        }

        public static IQueryable<SageAssignment> Assigned(this IQueryable<SageAssignment> query)
        {
            return query.Where(x => x.Employee != "");
        }

        public static IQueryable<SageAssignment> ToEmployee(this IQueryable<SageAssignment> query, string employeeId)
        {
            return query.Where(x => x.Employee == employeeId);
        }
    }
}