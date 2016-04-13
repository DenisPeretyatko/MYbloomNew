using MongoDB.Bson;

namespace BloomService.Domain.Entities
{
    public interface IEntity
    {
        string Id { get; set; }
    }
}
