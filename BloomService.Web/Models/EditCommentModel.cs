namespace BloomService.Web.Models
{
    public class EditCommentModel
    {
        public string Comment { get; set; }
        public long WorkOrder { get; set; }
        public long Id { get; set; }
    }
}