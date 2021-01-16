using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.ViewModels
{
    public class ProductViewModel : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
