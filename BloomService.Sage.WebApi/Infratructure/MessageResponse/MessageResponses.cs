namespace Sage.WebApi.Infratructure.MessageResponse
{
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class MessageResponses : SageEntity
    {
        public Response MessageResponse { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}