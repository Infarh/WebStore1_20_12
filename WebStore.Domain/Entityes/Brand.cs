//using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;
using WebStore.Domain.Entityes.Base;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes
{
    //[Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        //[Column("BrandOrder")]
        public int Order { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
