using System.Collections.Generic;

namespace BloomService.Web.Notifications
{
    public class NotificationAlert
    {
        public NotificationAlert()
        {
            Body = null;
            ActionLocalizedKey = null;
            LocalizedKey = null;
            LocalizedArgs = new List<object>();
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
                if (!string.IsNullOrEmpty(Body)
                    || !string.IsNullOrEmpty(ActionLocalizedKey)
                    || !string.IsNullOrEmpty(LocalizedKey)
                    || (LocalizedArgs != null && LocalizedArgs.Count > 0))
                    return false;
                else
                    return true;
            }
        }
    }
}