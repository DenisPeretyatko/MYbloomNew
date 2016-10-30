using System.Configuration;
using System;
using System.Collections.Specialized;
using System.Web.WebPages;

namespace BloomService.Web.Infrastructure
{
    public class BloomServiceConfiguration
    {
        public static BloomServiceConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                //Path mapped from URL
                var configuration = new BloomServiceConfiguration
                {
                    SageUsername = nameValueCollection["SageUsername"],
                    SagePassword = nameValueCollection["SagePassword"],
                    SyncDb = nameValueCollection["SyncDb"],
                    SageApiHost = nameValueCollection["SageApiHost"],
                    SiteUrl = nameValueCollection["SiteUrl"],
                    PushCertificateUrl = nameValueCollection["PushCertificateUrl"],
                    SizeBigPhoto = int.Parse(nameValueCollection["SizeBigPhoto"]),
                    SizeSmallPhoto = int.Parse(nameValueCollection["SizeSmallPhoto"]),
                    Timezone = nameValueCollection["Timezone"],
                    NotificationDelay = int.Parse(nameValueCollection["NotificationDelay"]),
                    CheckTechniciansDelay = int.Parse(nameValueCollection["CheckTechniciansDelay"]),
                    SynchronizationDelay = int.Parse(nameValueCollection["SynchronizationDelay"]),
                    Connection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString,
                    DbName = ConfigurationManager.AppSettings["DbName"],

                    AlertNotificationEnabled = bool.Parse(ConfigurationManager.AppSettings["AlertNotificationEnabled"]),
                    AlertBadgeNotificationEnabled = bool.Parse(ConfigurationManager.AppSettings["AlertBadgeNotificationEnabled"]),
                    AlertBadgeSoundNotificationEnabled = bool.Parse(ConfigurationManager.AppSettings["AlertBadgeSoundNotificationEnabled"]),

                    NotificationAlert = ConfigurationManager.AppSettings["NotificationAlert"],
                    NotificationBadge = int.Parse(ConfigurationManager.AppSettings["NotificationBadge"]),
                    NotificationSound = ConfigurationManager.AppSettings["NotificationSound"],
                    IngoreTechnicianAvaliability = bool.Parse(ConfigurationManager.AppSettings["IngoreTechnicianAvaliability"]),

                    BasePath = ConfigurationManager.AppSettings["BasePath"],
                    StorageUrl = ConfigurationManager.AppSettings["StorageUrl"],
                    UseAzureStorage = bool.Parse(ConfigurationManager.AppSettings["UseAzureStorage"])
                };

                return configuration;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }

        public string SageUsername { get; set; }
        public string SagePassword { get; set; }
        public string SyncDb { get; set; }
        public string SageApiHost { get; set; }
        public string SiteUrl { get; set; }

        public int SizeBigPhoto { get; set; }
        public int SizeSmallPhoto { get; set; }
        public string Timezone { get; set; }
        public int NotificationDelay { get; set; }
        public int CheckTechniciansDelay { get; set; }
        public int SynchronizationDelay { get; set; }
        public string DbName { get; set; }
        public string Connection { get; set; }

        public string PushCertificateUrl { get; set; }
        public bool AlertNotificationEnabled { get; set; }
        public bool AlertBadgeNotificationEnabled { get; set; }
        public bool AlertBadgeSoundNotificationEnabled { get; set; }

        public string NotificationAlert { get; set; }
        public int NotificationBadge { get; set; }
        public string NotificationSound { get; set; }
        public bool IngoreTechnicianAvaliability { get; set; }

        public bool UseAzureStorage { get; set; }
        public string BaseUrl { get; set; }
        public string BasePath { get; set; }
        public string StorageUrl { get; set; }

        public int ItemsOnPage => 50;
        public int NotificationOnPage = 9;
    }
}
