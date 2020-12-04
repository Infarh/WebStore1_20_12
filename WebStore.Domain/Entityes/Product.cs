using WebStore.Domain.Entityes.Base;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
