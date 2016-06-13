using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Notifications
{
    public class Feedback
    {
        public Feedback()
        {
            DeviceToken = string.Empty;
            Timestamp = DateTime.MinValue;
        }

        public string DeviceToken { get; set; }
        public DateTime Timestamp { get; set; }
    }
}