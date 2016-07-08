namespace BloomService.Web.Infrastructure.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Newtonsoft.Json.Linq;

    public class NotificationPayload
    {
        public NotificationAlert Alert { get; set; }
        public string DeviceToken { get; set; }
        public int? Badge { get; set; }
        public string Sound { get; set; }
        internal int PayloadId { get; set; }
        public int Content_available { get; set; }

        public Dictionary<string, object[]> CustomItems
        {
            get;
            private set;
        }

        public NotificationPayload(string deviceToken)
        {
            this.DeviceToken = deviceToken;
            this.Alert = new NotificationAlert();
            this.CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert)
        {
            this.DeviceToken = deviceToken;
            this.Alert = new NotificationAlert() { Body = alert };
            this.CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert, int badge)
        {
            this.DeviceToken = deviceToken;
            this.Alert = new NotificationAlert() { Body = alert };
            this.Badge = badge;
            this.CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert, int badge, string sound)
        {
            this.DeviceToken = deviceToken;
            this.Alert = new NotificationAlert() { Body = alert };
            this.Badge = badge;
            this.Sound = sound;

            this.CustomItems = new Dictionary<string, object[]>();

        }

        public NotificationPayload(string deviceToken, int content_available, string sound)
        {
            DeviceToken = deviceToken;
            Content_available = content_available;
            Sound = sound;
            CustomItems = new Dictionary<string, object[]>();
        }

        public void AddCustom(string key, params object[] values)
        {
            if (values != null)
                this.CustomItems.Add(key, values);
        }

        public string ToJson()
        {
            JObject json = new JObject();

            JObject aps = new JObject();

            if (this.Content_available == 1)
                aps["content-available"] = new JValue(this.Content_available);

            if (!string.IsNullOrEmpty(Sound))
                aps["sound"] = new JValue(Sound);

            json["aps"] = aps;


            foreach (string key in this.CustomItems.Keys)
            {
                if (this.CustomItems[key].Length == 1)
                    json[key] = new JValue(this.CustomItems[key][0]);
                else if (this.CustomItems[key].Length > 1)
                    json[key] = new JArray(this.CustomItems[key]);
            }
            

            string rawString = json.ToString(Newtonsoft.Json.Formatting.None, null);

            StringBuilder encodedString = new StringBuilder();
            foreach (char c in rawString)
            {
                if ((int)c < 32 || (int)c > 127)
                    encodedString.Append("\\u" + String.Format("{0:x4}", Convert.ToUInt32(c)));
                else
                    encodedString.Append(c);
            }
            return rawString;// encodedString.ToString();
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}