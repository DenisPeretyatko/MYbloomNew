using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    public abstract class SageEntity : IEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}