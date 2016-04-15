namespace BloomService.Domain.Entities.MessageResponse
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class Response
    {
        public ResponseError Error { get; set; }

        public ResponseReturnParams ReturnParams { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string TargetId { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}