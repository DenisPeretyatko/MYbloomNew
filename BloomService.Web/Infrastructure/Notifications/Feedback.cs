namespace BloomService.Web.Infrastructure.Notifications
{
    using System;

    public class Feedback
    {
        public Feedback()
        {
            this.DeviceToken = string.Empty;
            this.Timestamp = DateTime.MinValue;
        }

        public string DeviceToken { get; set; }
        public DateTime Timestamp { get; set; }
    }
}