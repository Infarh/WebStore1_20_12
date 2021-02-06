using System;
using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }
        
        public int? SectionId { get; init; }
        
        public int? BrandId { get; init; }

        public PageViewModel PageViewModel { get; set; }
    }

    public class PageViewModel
    {
        public int Page { get; init; }

        public int PageSize { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages => PageSize == 0 ? 0 : (int) Math.Ceiling((double) TotalItems / PageSize);
    }
}
