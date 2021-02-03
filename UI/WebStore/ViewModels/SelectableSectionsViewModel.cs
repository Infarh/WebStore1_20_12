using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.ViewModels
{
    public record SelectableSectionsViewModel(IEnumerable<SectionViewModel> Sections, int? SectionId, int? ParentSectionId)
    {
        //public IEnumerable<SectionViewModel> Sections { get; set; }

        //public int? SectionId { get; set; }

        //public int? ParentSectionId { get; set; }
    }
}
