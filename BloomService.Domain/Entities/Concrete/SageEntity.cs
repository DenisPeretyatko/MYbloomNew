namespace BloomService.Domain.Entities.Concrete
{
    using BloomService.Domain.Entities.Abstract;

    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class SageEntity : IEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}