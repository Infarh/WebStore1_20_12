namespace WebStore.Domain.Entityes.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}