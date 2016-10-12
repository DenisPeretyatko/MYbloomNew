using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class ChartModel
    {
        public List<Chart> data { get; set; }
    }

    public class Chart
    {
        public int data { get; set; }
        public string label { get; set; }
        public string color { get; set; }
    }
}