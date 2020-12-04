using System.Collections.Generic;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<SectionViewModel> ChildSections { get; set; }

        public SectionViewModel ParentSection { get; set; }
    }
}
