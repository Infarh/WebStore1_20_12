using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class BrandViewModel : INamedEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
