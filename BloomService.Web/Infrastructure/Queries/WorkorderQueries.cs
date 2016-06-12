using BloomService.Domain.Entities.Concrete;
using System.Linq;

namespace BloomService.Web.Infrastructure.Queries
{
    public static class WorkorderQueries
    {
        public static IQueryable<SageWorkOrder> Open(this IQueryable<SageWorkOrder> query)
        {
            return query.Where(x => x.Status == "Open");
        }
    }
}