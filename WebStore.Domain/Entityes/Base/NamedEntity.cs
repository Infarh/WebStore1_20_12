using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
