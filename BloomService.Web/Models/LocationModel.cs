using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class LocationModel
    {
        [JsonProperty("SiteType")]
        public string SiteType { get; set; }
        [JsonProperty("Location")]
        public string Location { get; set; }
        [JsonProperty("Alias")]
        public string Alias { get; set; }
        [JsonProperty("ARCustomer")]
        public string ArCustomer { get; set; }
        [JsonProperty("MainFax")]
        public string MainFax { get; set; }
        [JsonProperty("MainPhone")]
        public string MainPhone { get; set; }
        [JsonProperty("Contact1Name")]
        public string Contact1Name { get; set; }
        [JsonProperty("Contact1Title")]
        public string Contact1Title { get; set; }
        [JsonProperty("Contact1Phone")]
        public string Contact1Phone { get; set; }
        [JsonProperty("Contact1Mobile")]
        public string Contact1Mobile { get; set; }
        [JsonProperty("Contact1Fax")]
        public string Contact1Fax { get; set; }
        [JsonProperty("ProContact1Emailblem")]
        public string Contact1Email { get; set; }
        [JsonProperty("Contact2Name")]
        public string Contact2Name { get; set; }
        [JsonProperty("Contact2Title")]
        public string Contact2Title { get; set; }
        [JsonProperty("Contact2Phone")]
        public string Contact2Phone { get; set; }
        [JsonProperty("Contact2Mobile")]
        public string Contact2Mobile { get; set; }
        [JsonProperty("Contact2Fax")]
        public string Contact2Fax { get; set; }
        [JsonProperty("Contact2Email")]
        public string Contact2Email { get; set; }
        [JsonProperty("GLPrefix")]
        public string GlPrefix { get; set; }
        [JsonProperty("JCJob")]
        public string JcJob { get; set; }
        [JsonProperty("JCExtra")]
        public string JcExtra { get; set; }
        [JsonProperty("LocationKey")]
        public string LocationKey { get; set; }
        [JsonProperty("Center")]
        public string Center { get; set; }
        [JsonProperty("Area")]
        public string Area { get; set; }
        [JsonProperty("CustomerType")]
        public string CustomerType { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("Address2")]
        public string Address2 { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("ZIP")]
        public string Zip { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("SalesEmployee")]
        public string SalesEmployee { get; set; }
        [JsonProperty("Employee")]
        public string Employee { get; set; }
        [JsonProperty("AccountOpenDate")]
        public string AccountOpenDate { get; set; }
        [JsonProperty("MapLocation")]
        public string MapLocation { get; set; }
        [JsonProperty("BilloutAuthorization")]
        public string BilloutAuthorization { get; set; }
        [JsonProperty("TaxGroup")]
        public string TaxGroup { get; set; }
        [JsonProperty("PayTerms")]
        public string PayTerms { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("RateSheet")]
        public string RateSheet { get; set; }
        [JsonProperty("ExemptStatus")]
        public string ExemptStatus { get; set; }
        [JsonProperty("ABN")]
        public string Abn { get; set; }
        [JsonProperty("Miscellaneous")]
        public string Miscellaneous { get; set; }
        [JsonProperty("Memo")]
        public string Memo { get; set; }
        [JsonProperty("PermissionCode")]
        public string PermissionCode { get; set; }
        [JsonProperty("PREmployee")]
        public string PrEmployee { get; set; }
    }
}