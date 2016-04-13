using MongoDB.Bson;

namespace BloomService.Domain.Entities
{
    public abstract class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
