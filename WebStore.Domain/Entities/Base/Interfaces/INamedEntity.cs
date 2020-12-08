namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}