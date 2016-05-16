namespace BloomService.Domain.Entities.Concrete
{
    using BloomService.Domain.Entities.Abstract;

    using MongoDB.Bson.Serialization.Attributes;
    using System.Xml.Serialization;
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class SageEntity : IEntity
    {
        [XmlIgnore]
        [BsonId]
        public string Id { get; set; }
    }
}