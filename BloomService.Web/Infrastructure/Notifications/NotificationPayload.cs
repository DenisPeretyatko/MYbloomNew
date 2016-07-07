using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloomService.Web.Notifications
{
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
            DeviceToken = deviceToken;
            Alert = new NotificationAlert();
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert)
        {
            DeviceToken = deviceToken;
            Alert = new NotificationAlert() { Body = alert };
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert, int badge)
        {
            DeviceToken = deviceToken;
            Alert = new NotificationAlert() { Body = alert };
            Badge = badge;
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken, string alert, int badge, string sound)
        {
            DeviceToken = deviceToken;
            Alert = new NotificationAlert() { Body = alert };
            Badge = badge;
            Sound = sound;

            CustomItems = new Dictionary<string, object[]>();

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
                CustomItems.Add(key, values);
        }

        public string ToJson()
        {
            JObject json = new JObject();

            JObject aps = new JObject();

            if (Content_available == 1)
                aps["content-available"] = new JValue(Content_available);

            json["aps"] = aps;

            foreach (string key in CustomItems.Keys)
            {
                if (CustomItems[key].Length == 1)
                    json[key] = new JValue(CustomItems[key][0]);
                else if (CustomItems[key].Length > 1)
                    json[key] = new JArray(CustomItems[key]);
            }
            if (!string.IsNullOrEmpty(Sound))
                aps["sound"] = new JValue(Sound);

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
            return ToJson();
        }
    }
}