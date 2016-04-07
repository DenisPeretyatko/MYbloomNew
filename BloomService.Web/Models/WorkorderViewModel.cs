using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Models
{
    public class WorkorderViewModel
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public int Hours { get; set; }
        public string Status { get; set; }
    }
}