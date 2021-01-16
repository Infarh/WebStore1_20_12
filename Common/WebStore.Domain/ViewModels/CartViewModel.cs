using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;

        public decimal TotalPrice => Items?.Sum(item => item.Product.Price * item.Quantity) ?? 0m;
    }
}
