using System;
using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class TechnicianModel
    {
        public string Id { get; set; }
        public Dictionary<string, DateTime> AvailableDays { get; set; }
        public bool IsAvailable { get; set; }
        public string Picture { get; set; }
    }
}