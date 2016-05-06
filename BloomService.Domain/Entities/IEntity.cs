using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    public interface IEntity
    {
        [BsonId]
        string Id { get; set; }
    }
}