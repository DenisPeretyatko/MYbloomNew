namespace BloomService.Domain.Entities
{
    using MongoDB.Bson;

    public abstract class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}