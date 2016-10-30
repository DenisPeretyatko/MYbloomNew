using System;
using System.Collections.Specialized;

namespace Sage.WebApi.Infratructure
{
    public class SageWebConfig
    {
        public string CatalogPath { get; set; }

        public string TimberlineDataConnectionString { get; private set; }

        public string TimberlineServiceManagementConnectionString { get; private set; }

        public string SageUserName { get; set; }

        public string SagePassword { get; set; }

        public string CurrentTimeZone { get; set; }

        public static SageWebConfig FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                // Path mapped from URL
                var configuration = new SageWebConfig
                {
                    CatalogPath = nameValueCollection["catalogPath"], 
                    TimberlineDataConnectionString = nameValueCollection["connectionString"], 
                    TimberlineServiceManagementConnectionString = nameValueCollection["TimberlineServiceManagementConnectionString"],
                    SageUserName = nameValueCollection["SageUsername"],
                    SagePassword = nameValueCollection["SagePassword"],
                    CurrentTimeZone = nameValueCollection["CurrentTimezone"]
                };

                return configuration;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }
    }
}