namespace Sage.WebApi.Infratructure
{
    using System;
    using System.Collections.Specialized;

    public class SageWebConfig
    {
        public string CatalogPath { get; set; }

        public string TimberlineDataConnectionString { get; private set; }

        public string TimberlineServiceManagementConnectionString { get; private set; }

        // public SageWebConfig()
        // {
        // CatalogPath = ConfigurationManager.AppSettings["catalogPath"];
        // TimberlineDataConnectionString = ConfigurationManager.AppSettings["connectionString"];
        // TimberlineServiceManagementConnectionString = "DSN=Timberline Service Management;";
        // }
        public static SageWebConfig FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                // Path mapped from URL
                var configuration = new SageWebConfig
                                        {
                                            CatalogPath = nameValueCollection["catalogPath"], 
                                            TimberlineDataConnectionString =
                                                nameValueCollection["connectionString"], 
                                            TimberlineServiceManagementConnectionString =
                                                nameValueCollection[
                                                    "TimberlineServiceManagementConnectionString"]
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