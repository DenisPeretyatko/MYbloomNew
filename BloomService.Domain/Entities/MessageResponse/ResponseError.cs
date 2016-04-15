namespace BloomService.Domain.Entities.MessageResponse
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class ResponseError
    {
        [XmlAttribute]
        public byte Code { get; set; }

        [XmlAttribute]
        public string Message { get; set; }
    }
}