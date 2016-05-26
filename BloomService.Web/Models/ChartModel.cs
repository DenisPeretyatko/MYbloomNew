using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class ChartModel
    {
        public List<List<object>> data { get; set; }
    }

    public class Chart
    {
        public int Data { get; set; }
        public string Description { get; set; }
    }
}