using BloomService.Domain.Entities.Concrete;
using System.Linq;

namespace BloomService.Web.Infrastructure.Queries
{
    public static class PartsQueries
    {
        public static IQueryable<SagePart> Avaliable(this IQueryable<SagePart> query)
        {
            return query.Where(x => x.PartNumber.StartsWith("R-") && x.Inactive == "No");
        }
    }
}