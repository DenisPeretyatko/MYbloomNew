using System;

namespace BloomService.Domain.Entities.Concrete
{
    public class SageNote
    {
        public long Type { get; set; }
        public long TransNbr { get; set; }
        public long TransNbr2 { get; set; }
        public long TransNbr3 { get; set; }
        public DateTime Date { get; set; }
        public long EmployeeNbr { get; set; }
        public DateTime DateEnter { get; set; }
        public long Format { get; set; }
        public long DatabaseNbr { get; set; }
        public long Notenbr { get; set; }
        public string QSysGen { get; set; }
        public string QCustView { get; set; }
        public string Spare { get; set; }
        public string SubjectLine { get; set; }
        public string Text { get; set; }
    }
}