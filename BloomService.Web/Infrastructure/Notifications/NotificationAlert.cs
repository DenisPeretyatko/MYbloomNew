namespace BloomService.Web.Infrastructure.Notifications
{
    using System.Collections.Generic;

    public class NotificationAlert
    {
        public NotificationAlert()
        {
            this.Body = null;
            this.ActionLocalizedKey = null;
            this.LocalizedKey = null;
            this.LocalizedArgs = new List<object>();
        }

        public string Body { get; set; }
        public string ActionLocalizedKey { get; set; }
        public string LocalizedKey { get; set; }
        public List<object> LocalizedArgs { get; set; }

        public void AddLocalizedArgs(params object[] values)
        {
            this.LocalizedArgs.AddRange(values);
        }

        public bool IsEmpty
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Body)
                    || !string.IsNullOrEmpty(this.ActionLocalizedKey)
                    || !string.IsNullOrEmpty(this.LocalizedKey)
                    || (this.LocalizedArgs != null && this.LocalizedArgs.Count > 0))
                    return false;
                else
                    return true;
            }
        }
    }
}