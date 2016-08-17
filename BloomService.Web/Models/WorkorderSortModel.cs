namespace BloomService.Web.Models
{
	public class WorkorderSortModel
	{
        public string Search { get; set; }
        public string Column { get; set; }
        public bool Direction { get; set; }
        public int Index { get; set; }
    }
}