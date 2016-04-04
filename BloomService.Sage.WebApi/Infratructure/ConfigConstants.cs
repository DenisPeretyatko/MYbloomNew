namespace Sage.WebApi.Infratructure
{
    using System.Configuration;

    public class SageWebConfig
    {
        public string CatalogPath { get; }
        public string ConnectionString { get; }
        public SageWebConfig()
        {
            CatalogPath = ConfigurationManager.AppSettings["catalogPath"];
            ConnectionString = ConfigurationManager.AppSettings["connectionString"];
        }
    }
}