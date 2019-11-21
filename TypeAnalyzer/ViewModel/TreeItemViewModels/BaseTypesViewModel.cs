using System.Collections.Generic;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  class BaseTypesViewModel : TypesViewModel
  {
    public BaseTypesViewModel(IEnumerable<TypeMetadata> types) : base(types, "Base types") { }
  }
}
