using System.Collections.Generic;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
    public class DashboardViewModel
    {
        public List<ChartModel> Chart { get; set; }
        public IEnumerable<SageWorkOrder> WorkOrders { get; set; }
    }
}