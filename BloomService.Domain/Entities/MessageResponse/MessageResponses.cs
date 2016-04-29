namespace BloomService.Domain.Entities.MessageResponse
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class MessageResponses : SageEntity
    {
        public Response MessageResponse { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}