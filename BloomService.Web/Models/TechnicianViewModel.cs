using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Models
{
    public class TechnicianViewModel
    {
        public string Number { get; set; }
        public string FullName { get; set; }
        public bool IsAvailable { get; set; }
    }
}