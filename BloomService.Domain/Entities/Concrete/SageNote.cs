using System;

namespace BloomService.Domain.Entities.Concrete
{
    public class SageNote
    {
        public long TYPE { get; set; }
        public long TRANSNBR { get; set; }
        public long TRANSNBR2 { get; set; }
        public long TRANSNBR3 { get; set; }
        public DateTime? DATE { get; set; }
        public long EMPLOYEENBR { get; set; }
        public DateTime? DATEENTER { get; set; }
        public long FORMAT { get; set; }
        public long DATABASENBR { get; set; }
        public long NOTENBR { get; set; }
        public string QSYSGEN { get; set; }
        public string QCUSTVIEW { get; set; }
        public string SPARE { get; set; }
        public string SUBJECTLINE { get; set; }
        public string TEXT { get; set; }
    }
}