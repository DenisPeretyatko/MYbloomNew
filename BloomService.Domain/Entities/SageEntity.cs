namespace BloomService.Domain.Entities
{
    public abstract class SageEntity : IEntity
    {
        string IEntity.Id { get; set; }
    }
}