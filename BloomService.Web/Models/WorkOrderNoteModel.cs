namespace BloomService.Web.Models
{
    public class WorkOrderNoteModel
    {
        public long WorkOrderId { get; set; }
        public long NoteId { get; set; }
        public string SubjectLine { get; set; }
        public string Text { get; set; }
    }
}