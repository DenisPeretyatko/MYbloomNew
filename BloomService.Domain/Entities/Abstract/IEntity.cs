namespace BloomService.Domain.Entities.Abstract
{
    using MongoDB.Bson.Serialization.Attributes;

    public interface IEntity
    {
        [BsonId]
        string Id { get; set; }
    }
}