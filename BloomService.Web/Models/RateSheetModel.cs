namespace BloomService.Web.Models
{
    public class RateSheetModel : EntityModel
    {
        public long RATESHEETNBR { get; set; }
        public string DESCRIPTION { get; set; }
        public double LABORRATE { get; set; }
        public double STOCKPARTSMARKUP { get; set; }
        public double MISCPARTSMARKUP { get; set; }
        public double MISCLABORMARKUP { get; set; }
        public double TRIPCHGAMT { get; set; }
        public double TRIPCHGPRODUCT { get; set; }
        public string TRIPCHGDESC { get; set; }
        public double TRIPOPTION { get; set; }
        public string QINACTIVE { get; set; }
        public string SPARE { get; set; }
    }
}