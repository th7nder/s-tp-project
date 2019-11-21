using System.Collections.Generic;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class NestedTypesViewModel : TypesViewModel
  {
    public NestedTypesViewModel(IEnumerable<TypeMetadata> types) : base(types, "Nested types") { }
  }
}