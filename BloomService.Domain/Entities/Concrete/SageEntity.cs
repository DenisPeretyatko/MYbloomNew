namespace BloomService.Domain.Entities.Concrete
{
    using BloomService.Domain.Entities.Abstract;

    using MongoDB.Bson.Serialization.Attributes;

    public abstract class SageEntity : IEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}