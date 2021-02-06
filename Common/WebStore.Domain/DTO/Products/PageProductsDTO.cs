using System.Collections;
using System.Collections.Generic;

namespace WebStore.Domain.DTO.Products
{
    public record PageProductsDTO(IEnumerable<ProductDTO> Products, int TotalCount) : IEnumerable<ProductDTO>
    {
        public IEnumerator<ProductDTO> GetEnumerator() => Products.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Products).GetEnumerator();
    }
}
