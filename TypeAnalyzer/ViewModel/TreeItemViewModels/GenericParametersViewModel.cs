using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
    public class GenericParametersViewModel : TreeItemViewModel
    {
        private readonly IEnumerable<TypeMetadata> _genericTypes;

        public GenericParametersViewModel(IEnumerable<TypeMetadata> genericTypes, string name = "Generic parameters")
        {
            Name = name;
            _genericTypes = genericTypes;
        }

        protected override void BuildMyself()
        {
            IEnumerable<TypeViewModel> genericTypes = from genericType in _genericTypes
                                                      select new TypeViewModel(genericType);
            
            foreach (TypeViewModel genericType in genericTypes)
            {
                Children.Add(genericType);
            }
        }
    }
}