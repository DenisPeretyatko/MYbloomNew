namespace BloomService.Web.Models
{
    public class WorkorderViewModel
    {
        public int Number { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public decimal Hours { get; set; }
        public string Status { get; set; }
    }
}