using BloomService.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloomService.Web.Infrastructure.Queries
{
    public static class WorkorderQueries
    {
        public static IQueryable<SageWorkOrder> Open(this IQueryable<SageWorkOrder> query)
        {
            return query.Where(x => x.Status == "Open");
        }

        public static List<SageWorkOrder> ForDate(this IQueryable<SageWorkOrder> query, DateTime date)
        {
            var result = new List<SageWorkOrder>();
            foreach (var x in query)
            {
                if (x.DateEntered.HasValue && x.DateEntered.Value == date.Date)
                    result.Add(x);
            }
            return result;
        }
    }
}